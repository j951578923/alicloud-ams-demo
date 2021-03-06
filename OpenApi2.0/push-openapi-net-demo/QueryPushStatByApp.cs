using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Push.Model.V20160801;
using System;

namespace AlibabaCloud
{
    class QueryAppPushStat
    {
        static void Main()
        {
            IClientProfile clientProfile = DefaultProfile.GetProfile("cn-hangzhou", "<your access key id>", "<your access key secret>");
            DefaultAcsClient client = new DefaultAcsClient(clientProfile);
            QueryPushStatByAppRequest request = new QueryPushStatByAppRequest();
            request.AppKey = <Your AppKey>;

            request.Granularity = "DAY"; //DAY: 天粒度
            String startTime = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");//查询近期天的数据
            String endTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
            request.StartTime = startTime;
            request.EndTime = endTime;

            try
            {
                QueryPushStatByAppResponse response = client.GetAcsResponse(request);
                Console.WriteLine("RequestId:" + response.RequestId);
                foreach (QueryPushStatByAppResponse.AppPushStat stat in response.AppPushStats)
                {
                    Console.WriteLine("MessageId:" + stat.Time);
                    Console.WriteLine("SentCount:" + stat.SentCount);
                    Console.WriteLine("ReceivedCount:" + stat.ReceivedCount);
                    Console.WriteLine("OpenedCount:" + stat.OpenedCount);
                    Console.WriteLine("DeletedCount:" + stat.DeletedCount);
                }
                Console.ReadLine();
            }
            catch (ServerException e)
            {
                Console.WriteLine(e.ErrorCode);
                Console.WriteLine(e.ErrorMessage);
                Console.ReadLine();
            }
            catch (ClientException e)
            {
                Console.WriteLine(e.ErrorCode);
                Console.WriteLine(e.ErrorMessage);
                Console.ReadLine();
            }
        }

    }
}
