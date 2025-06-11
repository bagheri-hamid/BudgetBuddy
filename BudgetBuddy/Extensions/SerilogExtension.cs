using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace BudgetBuddy.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        // Get the connection string for Serilog from configuration
        var serilogDbConnection = builder.Configuration.GetConnectionString("SqlServerConnectionString");

        // These options map Serilog properties to SQL table columns.
        var columnOptions = new ColumnOptions();
        columnOptions.Store.Remove(StandardColumn.Properties); // Remove default Properties column
        columnOptions.Store.Add(StandardColumn.LogEvent); // Keep the full LogEvent JSON
        
        // Create an instance of MSSqlServerSinkOptions
        var sinkOptions = new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true,
            SchemaName = "log",
            BatchPostingLimit = 10,
        };

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration) // Read base Serilog config from appsettings
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                connectionString: serilogDbConnection,
                sinkOptions: sinkOptions,
                columnOptions: columnOptions,
                restrictedToMinimumLevel: LogEventLevel.Information
            )
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}