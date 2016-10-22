using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QoalaWS.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Index()
        {
            var ass = System.Reflection.Assembly.GetExecutingAssembly();

            return Ok(new
            {
                Version = ass.GetName().Version,
                CreateTime = System.IO.File.GetCreationTimeUtc(ass.Location),
                LastWriteTime = System.IO.File.GetLastWriteTimeUtc(ass.Location),
                ExportedTypes = ass.ExportedTypes.Select(a => a.FullName),
            });
        }

        [HttpGet]
        [Route("pull/log")]
        public IHttpActionResult PullLog()
        {
            String path = @"C:\\inetpub\\wwwroot\\" + "deploy.log";
            if (File.Exists(path))
                return Ok(File.ReadAllText(path));
            else
                return Ok("no log for you");
        }
        [HttpGet]
        [Route("pull/log/delete")]
        public IHttpActionResult PullLogDelete()
        {
            String path = @"C:\\inetpub\\wwwroot\\" + "deploy.log";
            if (File.Exists(path))
                File.Delete(path);
            return Ok();
        }

        [HttpGet]
        [Route("pull")]
        public IHttpActionResult Pull()
        {
            var proc = new System.Diagnostics.Process();

            try
            {
                String path = @"C:\\inetpub\\wwwroot\\";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = path + "deploy.cmd";
                proc.StartInfo.WorkingDirectory = path;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;

                Boolean iniciou = proc.Start();
                proc.WaitForExit(60 * 1000);

                string log = "", logerror = "", logout = "";

                try
                {
                    logerror += proc.StandardOutput.ReadToEnd();
                }
                catch { }
                try
                {
                    logout += proc.StandardError.ReadToEnd();
                }
                catch { }
                try
                {
                    log += System.IO.File.ReadAllText(path + "deploy.log");
                }
                catch { }
                return Ok(new
                {
                    totalTime = proc.TotalProcessorTime,
                    exitCode = proc.ExitCode,
                    exitTime = proc.ExitTime,
                    logError = logerror,
                    logOutput = logout,
                    log = log,
                });
            }
            catch (Exception ex)
            {
                return Ok(new { Exception = ex });
            }
            finally
            {
                if (proc != null)
                {
                    proc.Close();
                }
            }
        }
    }
}
