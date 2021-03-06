using System;
using System.Web.UI;
using System.IO;

public partial class fckeditor_editor_plugins_media_MediaMp4 : System.Web.UI.Page
{
    public static string linkImage = "";
    public static string linkMedia = "";
    public static string OutPut = "";
    protected int UserID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        linkImage = "";
        linkMedia = "";
        OutPut = "";
        UserID = VatLid.Utils.getUserId(Session);
    }
    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        string IDImageRandom = DateTime.Now.ToString("hhmmss") + VatLid.DAL.getRandom();
        OutPut = "";
        if (FileUpload1.HasFile)
        {
            String FileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            String FileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            String SaveFileName = IDImageRandom + FileExt;
            FileExt = FileExt.ToLower();
            string fileName = FileUpload1.FileName;
            if (!fileName.EndsWith(".mp4"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('Chỉ được Upload tập tin mp4');", true);
            }
            if (FileExt == ".mp4")
            {
                try
                {

                    string path_to_folder = VatLid.Variables.sPathToResource + "archive/media2/" + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(path_to_folder))
                        Directory.CreateDirectory(path_to_folder);

                    FileUpload1.SaveAs(path_to_folder + "/" + SaveFileName);
                    String Url = VatLid.Variables.sLinkToResource + "archive/media2/" + DateTime.Now.ToString("yyyyMMdd") + "/" + SaveFileName;
                    linkMedia = Url;
                    VatLid.DAL.SyncImagesInsert(linkMedia);

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Update", @"document.getElementById('txtURL').value='" + Url.Replace("'", "") + "'; ", true);

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('" + ex.Message.Replace("'", "\'") + @"');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('Chỉ được Upload tập tin mp4');", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('Bạn chưa xác định tập tin cần Upload');", true);
        }


        if (FileUpload2.HasFile)
        {

            String FileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload2.FileName);
            String FileExt = System.IO.Path.GetExtension(FileUpload2.FileName);
            String SaveFileName = IDImageRandom + FileExt;
            FileExt = FileExt.ToLower();
            string fileName = FileUpload2.FileName;
            if (!fileName.EndsWith(".gif") && !fileName.EndsWith(".jpeg") && !fileName.EndsWith(".jpg") && !fileName.EndsWith(".png"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('Chỉ được Upload tập tin jpg,png,jpeg,gif');", true);
            }
            else if (FileExt == ".jpg" || FileExt == ".png" || FileExt == ".jpeg" || FileExt == ".gif")
            {
                try
                {
                    string path_to_folder = VatLid.Variables.sPathToResource + "archive/media2/" + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(path_to_folder))
                        Directory.CreateDirectory(path_to_folder);

                    FileUpload2.SaveAs(path_to_folder + "/" + SaveFileName);
                    String Url1 = VatLid.Variables.sLinkToResource + "archive/media2/" + DateTime.Now.ToString("yyyyMMdd") + "/" + SaveFileName;
                    linkImage = Url1;

                    VatLid.DAL.SyncImagesInsert(linkImage);
                    reSizeImage(linkImage);

                
                    OutPut += "<div sytle=\"width:100%\" class=\"hot_news_img\">";
                    OutPut += "<video id=\"content_video\" class=\"video-js vjs-default-skin\"  data-setup=\"{}\"  name=\"media\" preload=\"none\" controls=\"\" poster=\"" + linkImage + "\" style=\"width:100%\" tabindex=\"0\">";
                    OutPut += "<source type=\"video/mp4\" src=\"" + linkMedia + "\" ></source></video>";
                    //  OutPut += "<p class=\"p-player\">Nếu không xem được, mời bạn <a href=\"" + linkMedia3gp + "\" class=\"player-link\">Click vào đây</a></p>";                   
                    OutPut += "</div>";


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Update", @"document.getElementById('txtURL').value='" + OutPut + "'; ", true);
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "Update", @"document.getElementById('txtURL').value='" + OutPut + "'; ", true);                    
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('" + ex.Message.Replace("'", "\'") + @"');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", @"alert('Chỉ được Upload tập tin jpg,png,jpeg,gif');", true);
            }
        }

    }

    public void reSizeImage(string path)
    {
        try
        {
            byte[] data = null;
            string Imagecms = Variables.sLinkToResource;
            path = path.Replace(VatLid.Variables.sLinkToResource, "");
            FileInfo fInfo = new FileInfo(Variables.sPathToResource + path);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(Variables.sPathToResource + path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);


            MemoryStream img = new MemoryStream(data);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);
            //string Imagecms = Variables.sLinkToResource;
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 380, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 260, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 250, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 320, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 280, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 240, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 220, path));
            VatLid.DAL.SyncImagesInsert(Imagecms + VatLid.Utils.resizeImage(returnImage, 200, path));

            img.Dispose();
            br.Close();
            if (br is IDisposable)
                ((IDisposable)br).Dispose();

            if (fStream is IDisposable)
                ((IDisposable)fStream).Dispose();


        }
        catch (Exception ex) { }
    }



}
