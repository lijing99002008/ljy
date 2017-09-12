using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace error_message
{
   public class PLM_Journal_select
    {
        #region 字段信息

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自增长列
        public int id { get; set; }







        /// <summary>        /// 接口传递参数        /// </summary>        [MaxLength(1073741824)]
        public string Interface_parameter { get; set; }







        /// <summary>        /// 接口名称        /// </summary>        [MaxLength(400)]
        public string Interface_name { get; set; }

        /// <summary>        /// 错误次数        /// </summary>
        public int Error_num { get; set; }

        /// <summary>        /// 是否发送过邮件 0-未发送 1-已发送        /// </summary>
        public int mail { get; set; }

        /// <summary>        /// 发送邮件日期        /// </summary>
        public DateTime? mail_date { get; set; }







        /// <summary>        /// 发送邮件地址        /// </summary>        [MaxLength(400)]
        public string mailing_address { get; set; }








        /// <summary>        /// 错误信息        /// </summary>        [MaxLength(400)]
        public string error_message { get; set; }

        #endregion

    }
}
