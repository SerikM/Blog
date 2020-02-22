using Data.DynamoDb.Interfaces;
using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using System.Linq;

namespace Data.DynamoDb.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private const  string TableName = "BlogPosts";

        public BlogPostService(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public BlogPostModel GetBlogPostById(string id)
        {
            var table = Table.LoadTable(_dynamoDbClient, TableName);
            var filter = new QueryFilter("Id", QueryOperator.Equal, "0");
            var search = table.Query(filter);
            var documentSet = search.GetNextSetAsync().Result;
            var document = documentSet.FirstOrDefault();

           return new BlogPostModel()
            {
                PublishDate = document["PublishDate"].AsString(),
                Topic = document["Topic"].AsString(),
                Content = document["Content"].AsString()
            };
        }

        public IEnumerable<BlogPostModel> GetBlogPostsList() 
        {
            var blogPostList = new List<BlogPostModel>();
            var table = Table.LoadTable(_dynamoDbClient, TableName);
            var oneYearAgo = DateTime.UtcNow - TimeSpan.FromDays(365);
            var filter = new QueryFilter("Id", QueryOperator.Equal, "0");
            filter.AddCondition("PublishDate", QueryOperator.GreaterThan, oneYearAgo);
            var search = table.Query(filter);

            do
            {
                var documentSet = search.GetNextSetAsync().Result;
                documentSet.ForEach(d =>
                {
                    blogPostList.Add(new BlogPostModel()
                    {
                        Id = d["Id"].AsString(),
                        PublishDate = d["PublishDate"].AsString(),
                        Topic = d["Topic"].AsString(),
                        Content = d["Content"].AsString()
                    });
                });
            } while (!search.IsDone);

            return blogPostList;
        }



        public void CreateDynamoDbTable()
        {
            try
            {
                var request = new CreateTableRequest()
                {
                    AttributeDefinitions = new List<AttributeDefinition>()
                   {
                     new AttributeDefinition()
                     {
                        AttributeName = "Id",
                        AttributeType = "N"
                     },
                    new AttributeDefinition()
                     {
                        AttributeName = "Date",
                        AttributeType = "S"
                     }
                   }, 

                    KeySchema = new List<KeySchemaElement>(){
                      new KeySchemaElement()
                      {
                        AttributeName = "Id",
                        KeyType = "HASH" //Partition key
                      },
                      new KeySchemaElement()
                      {
                        AttributeName = "Date",
                        KeyType = "Range" // Sort key
                      }

                    },
                    ProvisionedThroughput = new ProvisionedThroughput()
                    {
                        ReadCapacityUnits = 5,
                        WriteCapacityUnits = 5
                    },
                    TableName = "BlogPosts"
                };

                var response = _dynamoDbClient.CreateTableAsync(request).Result;
                string status = null;
                do
                {
                    var res = _dynamoDbClient.DescribeTableAsync(new DescribeTableRequest() { TableName = "BlogPosts" });
                    status = res.Result.Table.TableStatus;
                }
                while (status != "ACTIVE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
