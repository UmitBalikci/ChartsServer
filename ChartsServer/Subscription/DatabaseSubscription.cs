using ChartsServer.Hubs;
using ChartsServer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace ChartsServer.Subscription
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }

    public class DatabaseSubscription<T> : IDatabaseSubscription where T: class, new()
    {
        IConfiguration _configuration;
        SqlTableDependency<T> _tableDependency;
        IHubContext<ChartHub> _hubContext;

        public DatabaseSubscription(IConfiguration configuration, IHubContext<ChartHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }
        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"), tableName);
            
            // Database de herhangi bir değişikilikte tetiklenecek metot
            _tableDependency.OnChanged += async (o, e) =>
            {
                await _hubContext.Clients.All.SendAsync("receiveMessage", "helloWorld");
            };

            // Herhangi bir hatayla karşılaşınca tetiklenecek metot
            _tableDependency.OnError += (o, e) =>
            {

            };
            
            _tableDependency.Start();
        }
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }

        // --> Auto oluşan metot
        //_tableDependency_OnChanged;
        //private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<T> e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
