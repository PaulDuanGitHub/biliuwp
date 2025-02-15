﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliBili.UWP.Api.User
{
    public class FollowAPI
    {
        /// <summary>
        /// 我的追番
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="status">0=全部，1=想看，2=在看，3=看过</param>
        /// <param name="pagesize">每页数量</param>
        /// <returns></returns>
        public ApiModel MyFollowBangumi(int page = 1, int status = 0, int pagesize = 20)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                baseUrl = "https://api.bilibili.com/pgc/app/follow/v2/bangumi",
                parameter = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"&pn={page}&ps={pagesize}&status={status}",
            };
            api.parameter += ApiUtils.GetSign(api.parameter, ApiUtils.AndroidKey);
            return api;
        }
        /// <summary>
        /// 我的追剧
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="status">0=全部，1=想看，2=在看，3=看过</param>
        /// <param name="pagesize">每页数量</param>
        /// <returns></returns>
        public ApiModel MyFollowCinema(int page = 1, int status = 0, int pagesize = 20)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                baseUrl = "https://api.bilibili.com/pgc/app/follow/v2/cinema",
                parameter = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"&pn={page}&ps={pagesize}&status={status}",
            };
            api.parameter += ApiUtils.GetSign(api.parameter, ApiUtils.AndroidKey);
            return api;
        }
        /// <summary>
        /// 收藏番剧
        /// </summary>
        /// <returns></returns>
        public ApiModel FollowSeason(string season_id)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = $"https://api.bilibili.com/pgc/app/follow/add",
                body = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"&season_id={season_id}"
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }
        /// <summary>
        /// 取消收藏番剧
        /// </summary>
        /// <returns></returns>
        public ApiModel CancelFollowSeason(string season_id)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = $"https://api.bilibili.com/pgc/app/follow/del",
                body = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"&season_id={season_id}"
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <returns></returns>
        public ApiModel SetSeasonStatus(string season_id, int status)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "https://api.bilibili.com/pgc/app/follow/status/update",
                body = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"&season_id={season_id}&status={status}"
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }

        /// <summary>
        /// 我的收藏夹/收藏的收藏夹
        /// </summary>
        /// <returns></returns>
        public ApiModel MyFavorite()
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                //baseUrl = "https://api.bilibili.com/medialist/gateway/base/space",
                baseUrl = "http://api.bilibili.com/x/v3/fav/folder/collected/list",
                parameter = $"up_mid={ApiHelper.GetUserId()}&pn=1&ps=100",
                headers = new Dictionary<string, string>()
            };
            api.headers.Add("Cookie", ApiHelper.GetSESSDATA());
            //api.parameter += ApiUtils.GetSign(api.parameter, ApiUtils.AndroidKey);
            return api;
        }

        /// <summary>
        /// 我创建的收藏夹
        /// </summary>
        /// <returns></returns>
        public ApiModel MyCreatedFavorite()
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                //baseUrl = "https://api.bilibili.com/medialist/gateway/base/created",
                baseUrl = "http://api.bilibili.com/x/v3/fav/folder/created/list-all",
                parameter = $"&up_mid={ApiHelper.GetUserId()}"
            };
            api.headers = new Dictionary<string, string>();
            api.headers.Add("Cookie", ApiHelper.GetSESSDATA());
            //api.parameter += ApiUtils.GetSign(api.parameter, ApiUtils.AndroidKey);
            return api;
        }

        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        /// <returns></returns>
        public ApiModel AddFavorite(List<string> fav_ids, string avid)
        {
            var ids = "";
            foreach (var item in fav_ids)
            {
                ids += item + ",";
            }
            ids = Uri.EscapeDataString(ids.TrimEnd(','));
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "http://api.bilibili.com/x/v3/fav/resource/deal",
                body = $"add_media_ids={ids}&rid={avid}&type=2&csrf={ApiHelper.GetCSRF()}"
            };
            api.headers = new Dictionary<string, string>();
            api.headers.Add("Cookie", ApiHelper.GetSESSDATA());
            api.headers.Add("Referer", "https://www.bilibili.com");
            //api.headers.Add("Cookie", ApiHelper.GetCSRF());
            //api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }


        /// <summary>
        /// 创建收藏夹
        /// </summary>
        /// <returns></returns>
        public ApiModel CreateFavorite(string title, bool privacy)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "https://api.bilibili.com/medialist/gateway/base/add",
                body = ApiUtils.MustParameter(ApiUtils.AndroidKey, true) + $"privacy={(privacy ? 1 : 0)}&title={Uri.EscapeDataString(title)}"
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }


        /// <summary>
        /// 收藏夹信息，含视频
        /// </summary>
        /// <returns></returns>
        public ApiModel FavoriteInfo(string fid, string keyword, int page = 1)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                //baseUrl = "https://api.bilibili.com/medialist/gateway/base/detail",
                baseUrl = "http://api.bilibili.com/x/v3/fav/resource/list",
                parameter = $"media_id={fid}&mid={ApiHelper.GetUserId()}&keyword={Uri.EscapeDataString(keyword)}&pn={page}&ps=20"
            };
            api.headers = new Dictionary<string, string>();
            api.headers.Add("Cookie", ApiHelper.GetSESSDATA());
            api.parameter += ApiUtils.GetSign(api.parameter, ApiUtils.AndroidKey);
            return api;
        }


        /// <summary>
        /// 收藏夹移除视频
        /// </summary>
        /// <returns></returns>
        public ApiModel RemoveFavorite(string fid, string avid)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "http://api.bilibili.com/x/v3/fav/resource/deal",
                body = $"del_media_ids={fid}&rid={avid}&type=2&csrf={ApiHelper.GetCSRF()}"
            };
            api.headers = new Dictionary<string, string>();
            api.headers.Add("Cookie", ApiHelper.GetSESSDATA());
            api.headers.Add("Referer", "https://www.bilibili.com");
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidKey);
            return api;
        }

    }
}
