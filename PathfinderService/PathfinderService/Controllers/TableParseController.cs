using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using StatBlockCommon;

namespace PathfinderService.Controllers
{
    public class TableParseController : ApiController
    {
        [Route("table/parse")]
        [HttpPost]
        public IHttpActionResult ParseTable([FromBody] TableParseInfo info)
        {
            TableParseReturn tableParseReturn = new TableParseReturn();
            TableParse tableParse = new TableParse();
            try
            {
                tableParseReturn.TableText = tableParse.ParseTable(info.RawTableString, info.SetBorder);
                tableParseReturn.Error = string.Empty;
            }
            catch(Exception ex)
            {
                tableParseReturn.Error = ex.Message;
                tableParseReturn.TableText = string.Empty;
            }

            return Ok(tableParseReturn);
        }
    }

    public class TableParseInfo
    {
        public bool SetBorder { get; set; }
        public string RawTableString { get; set; }
    }

    public class TableParseReturn
    {
        public string TableText { get; set; }
        public string Error { get; set; }
    }
}