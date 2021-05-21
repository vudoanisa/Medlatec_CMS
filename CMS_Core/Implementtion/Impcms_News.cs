using CMS_Core.Entity;
using CMS_Core.Interface;
using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CMS_Core.Implementtion
{
    public class Impcms_News : Icms_News
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_News(cms_News _News)
        {
            SqlConnection con = null;
            string result = string.Empty;
            try
            {
                con = new SqlConnection(Common.Common.getConnectionString());
                SqlCommand cmd = new SqlCommand("SP_cms_News_DeleteByPrimaryKeyNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsId", _News.newsId);
                con.Open();
                cmd.ExecuteNonQuery();
                result = _News.newsId.ToString();
                return result;
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News: " + _News.newsId + "  " + ex.ToString());
                return result = string.Empty;

            }
            finally
            {
                con.Close();
            }
        }

        public string Deletecms_News(int newsId)
        {
            SqlConnection con = null;
            string result = string.Empty;
            try
            {
                con = new SqlConnection(Common.Common.getConnectionString());
                SqlCommand cmd = new SqlCommand("SP_cms_News_DeleteByPrimaryKeyNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsId", newsId);
                con.Open();
                cmd.ExecuteNonQuery();
                result = newsId.ToString();
                return result;
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News: " + newsId + "  " + ex.ToString());
                return result = string.Empty;

            }
            finally
            {
                con.Close();
            }
        }

        public List<cms_News> GetAllcms_News()
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectAll", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }
        }

        public List<cms_News> GetAllcms_News(DateTime Tungay, DateTime Denngay, int SourceId, int ParrenId, int cateId, int newsTypeMessage, int userId, int status, string keyword)
        {
            try
            {                
                if (string.IsNullOrEmpty(keyword))
                {
                    keyword = string.Empty;
                }
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@Tungay", Tungay);
                    parm.Add("@Denngay", Denngay);
                    parm.Add("@SourceId", SourceId);
                    parm.Add("@ParrenId", ParrenId);                    
                    parm.Add("@newsTypeMessage", newsTypeMessage);
                    parm.Add("@userId", userId);
                    parm.Add("@status", status);
                    parm.Add("@keyword", keyword);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectPara_New", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> GetAllcms_NewsCTV(DateTime Tungay, DateTime Denngay, int SourceId, int ParrenId, int cateId, int newsTypeMessage, int userId, int status, string keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    keyword = string.Empty;
                }
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@Tungay", Tungay);
                    parm.Add("@Denngay", Denngay);
                    parm.Add("@SourceId", SourceId);
                    parm.Add("@ParrenId", ParrenId);
                    parm.Add("@newsTypeMessage", newsTypeMessage);
                    parm.Add("@userId", userId);
                    parm.Add("@status", status);
                    parm.Add("@keyword", keyword);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectPara_New", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> GetAllcms_NewsSearch(int ParrenId)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@ParrenId", ParrenId);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectSearch", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_NewsSearch: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> GetAllcms_NewsSearchNew(int ParrenId, string keyword)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@ParrenId", ParrenId);
                    parm.Add("@keyword", keyword);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectSearchNew", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_NewsSearch: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> GetAllcms_NewsRelate( int newsId)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();                  
                    parm.Add("@newsId", newsId);                   
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectRelate", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_NewsRelate: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> GetAllcms_NewsRelateTestcode(int Id)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@newsId", Id);
                    using (var mul = con.QueryMultiple("SP_cms_News_RelateTestcode_SelectRelate", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_NewsRelate: " + ex.ToString());

                return null;
            }

        }
        
        public List<cms_News> Getcms_NewsByID(int newsId)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@newsId", newsId);                    
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectByPrimaryKey", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }

        }

        public List<cms_News> Getcms_NewsByIDs(string newsId)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@newsIds", newsId);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectByWhereINPrimarykey", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }

        }


        public List<cms_News> Getcms_NewsByIDUser(int newsId, int userid)
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    parm.Add("@newsId", newsId);
                    parm.Add("@userid", userid);
                    using (var mul = con.QueryMultiple("SP_cms_News_SelectByIDandUserid", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_News>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News: " + ex.ToString());

                return null;
            }

        }

        public string Insertcms_News(cms_News _News)
        {
            SqlConnection con = null;
            string result = string.Empty;
            try
            {                 
                con = new SqlConnection(Common.Common.getConnectionString());
                SqlCommand cmd = new SqlCommand("SP_cms_News_Insert_New", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cateId", _News.cateId);
                cmd.Parameters.AddWithValue("@SourceId", _News.SourceId);
                cmd.Parameters.AddWithValue("@userId", _News.userId);
                cmd.Parameters.AddWithValue("@newsName ", _News.newsName);
                cmd.Parameters.AddWithValue("@newsKeyword", _News.newsKeyword);
                cmd.Parameters.AddWithValue("@newsDescription", _News.newsDescription);
                cmd.Parameters.AddWithValue("@newsContent", _News.newsContent);
                cmd.Parameters.AddWithValue("@newsImages", _News.newsImages);
                cmd.Parameters.AddWithValue("@newsImagesWeb", _News.newsImagesWeb);
                cmd.Parameters.AddWithValue("@newsTitleImages", _News.newsTitleImages);
                cmd.Parameters.AddWithValue("@newsAuthor", _News.newsAuthor);
                cmd.Parameters.AddWithValue("@dateCreate", DateTime.Now);
                cmd.Parameters.AddWithValue("@newsFile", _News.newsFile);
                cmd.Parameters.AddWithValue("@copyRight", _News.copyRight);
                cmd.Parameters.AddWithValue("@allowComment", _News.allowComment);
                cmd.Parameters.AddWithValue("@alowPrint", _News.alowPrint);
                cmd.Parameters.AddWithValue("@active", _News.active);
                cmd.Parameters.AddWithValue("@AccountTypeId", _News.AccountTypeId);                
                cmd.Parameters.AddWithValue("@ListNewsRelate", _News.ListNewsRelate);
                cmd.Parameters.AddWithValue("@Tags", _News.Tags);
                cmd.Parameters.AddWithValue("@UrlSource", _News.UrlSource);
                cmd.Parameters.AddWithValue("@datepub", _News.datepub);
                cmd.Parameters.AddWithValue("@MotaSeo", _News.MotaSeo);
                cmd.Parameters.AddWithValue("@cateIdList", _News.cateIdList);
                cmd.Parameters.AddWithValue("@Slug", _News.Slug);
                cmd.Parameters.AddWithValue("@newsContentAutoLink", _News.newsContentAutoLink);
                cmd.Parameters.AddWithValue("@LinksLimitPerPage", _News.LinksLimitPerPage);
                cmd.Parameters.AddWithValue("@OptionReplate", _News.OptionReplate);
               cmd.Parameters.AddWithValue("@doctor_ID", _News.doctor_ID);

                SqlParameter outparam = cmd.Parameters.Add("@newsId", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@newsId"].Value.ToString();
                return result;
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_News: " + ex.ToString());
                return result = string.Empty;

            }
            finally
            {
                con.Close();
            }
        }

        public string Publiccms_News(int newsId)
        {
            SqlConnection con = null;
            string result = string.Empty;
            try
            {
                con = new SqlConnection(Common.Common.getConnectionString());
                SqlCommand cmd = new SqlCommand("SP_cms_News_PublicByPrimaryKey", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsId", newsId);
                con.Open();
                cmd.ExecuteNonQuery();
                result = newsId.ToString();
                return result;
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_News: " + newsId + "  " + ex.ToString());
                return result = string.Empty;

            }
            finally
            {
                con.Close();
            }
        }

        public string Updatecms_News(cms_News _News)
        {
            if (string.IsNullOrEmpty(_News.MotaSeo))
                _News.MotaSeo = string.Empty;
            SqlConnection con = null;
            string result = string.Empty;
            try
            {
                con = new SqlConnection(Common.Common.getConnectionString());
                SqlCommand cmd = new SqlCommand("SP_cms_News_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cateId", _News.cateId);
                cmd.Parameters.AddWithValue("@SourceId", _News.SourceId);
                cmd.Parameters.AddWithValue("@userId", _News.userId);
                cmd.Parameters.AddWithValue("@newsName ", _News.newsName);
                cmd.Parameters.AddWithValue("@newsKeyword", _News.newsKeyword);
                cmd.Parameters.AddWithValue("@newsDescription", _News.newsDescription);
                cmd.Parameters.AddWithValue("@newsContent", _News.newsContent);
                cmd.Parameters.AddWithValue("@newsImages", _News.newsImages);
                cmd.Parameters.AddWithValue("@newsImagesWeb", _News.newsImagesWeb);
                cmd.Parameters.AddWithValue("@newsTitleImages", _News.newsTitleImages);
                cmd.Parameters.AddWithValue("@newsAuthor", _News.newsAuthor);                
                cmd.Parameters.AddWithValue("@newsFile", _News.newsFile);
                cmd.Parameters.AddWithValue("@copyRight", _News.copyRight);
                cmd.Parameters.AddWithValue("@allowComment", _News.allowComment);
                cmd.Parameters.AddWithValue("@alowPrint", _News.alowPrint);
                cmd.Parameters.AddWithValue("@active", _News.active);
                cmd.Parameters.AddWithValue("@AccountTypeId", _News.AccountTypeId);
                cmd.Parameters.AddWithValue("@ListNewsRelate", _News.ListNewsRelate);
                cmd.Parameters.AddWithValue("@Tags", _News.Tags);
                cmd.Parameters.AddWithValue("@UrlSource", _News.UrlSource);
                cmd.Parameters.AddWithValue("@datepub", _News.datepub);
                cmd.Parameters.AddWithValue("@MotaSeo", _News.MotaSeo);
                cmd.Parameters.AddWithValue("@newsId", _News.newsId);
                cmd.Parameters.AddWithValue("@cateIdList", _News.cateIdList);
                cmd.Parameters.AddWithValue("@Slug", _News.Slug);
                cmd.Parameters.AddWithValue("@newsContentAutoLink", _News.newsContentAutoLink);
                cmd.Parameters.AddWithValue("@LinksLimitPerPage", _News.LinksLimitPerPage);
                cmd.Parameters.AddWithValue("@OptionReplate", _News.OptionReplate);
                cmd.Parameters.AddWithValue("@doctor_ID", _News.doctor_ID);
                con.Open();
                cmd.ExecuteNonQuery();
                result = _News.newsId.ToString();
                return result;
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_News: " + ex.ToString());
                return result = string.Empty;

            }
            finally
            {
                con.Close();
            }
        }
    }
}
