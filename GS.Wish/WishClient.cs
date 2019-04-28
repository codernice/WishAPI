using GS.Wish.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GS.Wish
{
    public class WishClient
    {
        public string access_token = "";
        public WishClient(string _access_token)
        {
            access_token = _access_token;
        }

        /// <summary>
        /// 同步wish订单
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<SyncWishOrdersReturn> SyncOrders(SyncWishOrdersFilter filter)
        {
            using (var client = new WebClient())
            {
                List<SyncWishOrdersReturn> list = new List<SyncWishOrdersReturn>();
                string url = "https://china-merchant.wish.com/api/v2/order/multi-get?start={0}&limit={1}&since={2}&access_token={3}&show_original_shipping_detail=True";
                url = string.Format(url, filter.start, filter.limit, filter.since, access_token);
                var responseString = client.DownloadString(url);
                BaseReturn<List<SyncWishOrdersReturn>> entity = new BaseReturn<List<SyncWishOrdersReturn>>();
                entity = JsonConvert.DeserializeObject<BaseReturn<List<SyncWishOrdersReturn>>>(responseString);
                if (entity.code != 0)
                {
                    throw new Exception(entity.message);
                }
                list.AddRange(entity.data);
                while (entity.paging != null && entity.paging.next != null)
                {
                    responseString = client.DownloadString(entity.paging.next);
                    entity = new BaseReturn<List<SyncWishOrdersReturn>>();
                    entity = JsonConvert.DeserializeObject<BaseReturn<List<SyncWishOrdersReturn>>>(responseString);
                    if (entity.code == 0 && entity.data.Count() > 0)
                    {
                        list.AddRange(entity.data);
                    }
                    else
                    {
                        break;
                    }
                }
                list = list.OrderBy(q => q.Order.last_updated).ToList();
                return list;
            }
        }

        /// <summary>
        /// 回传物流信息到平台
        /// </summary>
        /// <param name="modifyTrackingDto"></param>
        public void ModifyTracking(ModifyTrackingDto modifyTrackingDto)
        {
            string url = "https://china-merchant.wish.com/api/v2/order/modify-tracking?access_token={0}&format=json&id={1}&tracking_provider={2}&origin_country_code={3}&tracking_number={4}";
            url = string.Format(url, access_token, modifyTrackingDto.OrderID, modifyTrackingDto.TrackingName, modifyTrackingDto.CountryCode, modifyTrackingDto.TrackingNumber);
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadData(url);
                    var responseString = Encoding.UTF8.GetString(response);
                    NormalReturn entity = new NormalReturn();
                    entity = JsonConvert.DeserializeObject<NormalReturn>(responseString);
                    if (entity.code != 0 && entity.code != 1014)
                    {
                        throw new Exception(entity.message);
                    }
                }
                catch (System.Net.WebException ex)
                {
                    var re = new StreamReader(ex.Response.GetResponseStream());
                    string str = re.ReadToEnd();
                    NormalReturn entity = new NormalReturn();
                    entity = JsonConvert.DeserializeObject<NormalReturn>(str);
                    if (entity.code != 0 && entity.code != 1014)
                    {
                        throw new Exception(entity.message);
                    }
                }
            }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public RefreshTokenReturn RefreshToken(string clientID, string clientSecret, string refreshToken)
        {
            string url = "https://merchant.wish.com/api/v2/oauth/refresh_token";
            var values = new NameValueCollection();
            values["client_id"] = clientID;
            values["client_secret"] = clientSecret;
            values["refresh_token"] = refreshToken;
            values["grant_type"] = "refresh_token";
            using (var client = new WebClient())
            {
                var response = client.UploadValues(url, values);
                var responseString = Encoding.UTF8.GetString(response);
                BaseReturn<RefreshTokenReturn> entity = new BaseReturn<RefreshTokenReturn>();
                entity = JsonConvert.DeserializeObject<BaseReturn<RefreshTokenReturn>>(responseString);
                if (entity.code != 0)
                {
                    throw new Exception(entity.message);
                }
                this.access_token = entity.data.access_token;
                return entity.data;
            }
        }
    }
}
