using System;
using Display.Models;
using MongoDB.Driver;

namespace Display.Data
{
    public class EmailRepository
    {
        private readonly MongoCollection<EmailModel> _collection;
        private readonly ConnectionHelper<EmailModel> _connectionHelper = new ConnectionHelper<EmailModel>();

        public EmailRepository()
        {
            var database = MongoDatabase.Create(_connectionHelper.ConnectionString);
            _collection = database.GetCollection<EmailModel>("Email");
        }

        public void Store(EmailModel entity)
        {
            try
            {
                _collection.Insert(entity);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            //var settings = MCAPISettings.ListAPISettings().ToList();
            //var parms = new listSubscribeParms
            //                {
            //                    double_optin = false,
            //                    apikey = MCAPISettings.default_apikey,
            //                    email_address = entity.Email,
            //                    email_type = EnumValues.emailType.NotSpecified,
            //                    id = "172ce80373",
            //                    replace_interests = false,
            //                    send_welcome = false
            //                };
            //var input = new listSubscribeInput(parms);
            //var command = new listSubscribe(input);
            //var output = command.Execute();
        }
    }
}