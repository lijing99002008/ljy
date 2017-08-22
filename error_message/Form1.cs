using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Bll;
using Dal;
using Helper;
using BaseClass;



namespace error_message
{
    public partial class Form1 : Form
    {
        List<PLM_Journal_Master> List_PLM_Journal_Master = new List<PLM_Journal_Master>();
        List<PLM_Journal> List_PLM_Journal = new List<PLM_Journal>();
        Bll.PLM_Interface_Bll PLM_Interface_Bll = new PLM_Interface_Bll();

        public Form1()
        {
            InitializeComponent();
        }

        #region 重新赋值List_PLM_Journal_Master&List_PLM_Journal
        private List<PLM_Journal_Master> Delete_duplicates(List<PLM_Journal_select> List_PLM_Journal_select)
        {
            List_PLM_Journal_Master.Clear();
                    
            List<PLM_Journal_select> List_PLM_Journal_select1 = new List<PLM_Journal_select>();

            foreach (PLM_Journal_select item in List_PLM_Journal_select)
            {
                    PLM_Journal_Master PLM_Journal_Master_add = new PLM_Journal_Master();
                    PLM_Journal_Master_add.error_message = item.error_message;
                    PLM_Journal_Master_add.Error_num = item.Error_num;
                    PLM_Journal_Master_add.Interface_name = item.Interface_name;
                    PLM_Journal_Master_add.Interface_parameter = item.Interface_parameter;
                    PLM_Journal_Master_add.mail = item.mail;
                    PLM_Journal_Master_add.mailing_address = item.mailing_address;
                    PLM_Journal_Master_add.mail_date = item.mail_date;
                    PLM_Journal_Master_add.id = item.id;
                    List_PLM_Journal_Master.Add(PLM_Journal_Master_add);

            }
            return List_PLM_Journal_Master;
        }

        private List<PLM_Journal_select> Delete_duplicates(List<PLM_Journal_Master> List_PLM_Journal_Master)
        {

            List<PLM_Journal_select> List_PLM_Journal_select1 = new List<PLM_Journal_select>();

            foreach (PLM_Journal_Master item in List_PLM_Journal_Master)
            {
                PLM_Journal_select PLM_Journal_select_add = new PLM_Journal_select();
                PLM_Journal_select_add.error_message = item.error_message;
                PLM_Journal_select_add.Error_num = item.Error_num;
                PLM_Journal_select_add.Interface_name = item.Interface_name;
                PLM_Journal_select_add.Interface_parameter = item.Interface_parameter;
                PLM_Journal_select_add.mail = item.mail;
                PLM_Journal_select_add.mailing_address = item.mailing_address;
                PLM_Journal_select_add.mail_date = item.mail_date;
                PLM_Journal_select_add.id = item.id;
                List_PLM_Journal_select1.Add(PLM_Journal_select_add);

            }
            return List_PLM_Journal_select1;
        }

        private List<PLM_Journal> Delete_duplicates_detail(List<PLM_Journal_select_detail> List_PLM_Journal_select_detail)
        {

            List_PLM_Journal.Clear();
            //List < PLM_Journal_select_detail > PLM_Journal_select_detail = new List<PLM_Journal_select_detail>();

            foreach (PLM_Journal_select_detail item in List_PLM_Journal_select_detail)
            {
                PLM_Journal PLM_Journal_add = new PLM_Journal();
                PLM_Journal_add.error_message = item.error_message;
                PLM_Journal_add.Interface_name = item.Interface_name;
                PLM_Journal_add.Interface_parameter = item.Interface_parameter;
                PLM_Journal_add.id = item.id;
                PLM_Journal_add.time = item.time;
                List_PLM_Journal.Add(PLM_Journal_add);
            }
            return List_PLM_Journal;
        }

        private List<PLM_Journal_select_detail> Delete_duplicates_detail_2(List<PLM_Journal> List_PLM_Journal)
        {


            List<PLM_Journal_select_detail> List_PLM_Journal_select_detail = new List<PLM_Journal_select_detail>();

            foreach (PLM_Journal item in List_PLM_Journal)
            {
                PLM_Journal_select_detail PLM_Journal_select_detail = new PLM_Journal_select_detail();
                PLM_Journal_select_detail.error_message = item.error_message;
                PLM_Journal_select_detail.Interface_name = item.Interface_name;
                PLM_Journal_select_detail.Interface_parameter = item.Interface_parameter;
                PLM_Journal_select_detail.id = item.id;
                PLM_Journal_select_detail.time = item.time;
                List_PLM_Journal_select_detail.Add(PLM_Journal_select_detail);
            }
            return List_PLM_Journal_select_detail;
        }


        #endregion

        #region 比较器（需调用声明）

        private void Form1_Load(object sender, EventArgs e)
        {
            PLM_Interface_Bll PLM_Interface_Bll = new PLM_Interface_Bll();

            List_PLM_Journal = PLM_Interface_Bll.select_PLM_Journal();
            List_PLM_Journal_Master = PLM_Interface_Bll.select_PLM_Journal_Master();

            string Interface_nam = "";
            foreach (var item in List_PLM_Journal_Master)
            {
                if (Interface_nam != item.Interface_name)
                {
                    Interface_nam = item.Interface_name;
                    this.comboBox1.Items.Add(Interface_nam);
                }
            }

        }

        #endregion

        #region 子表显示

        /// <summary>
        /// 子表显示
        /// </summary>
        /// <param name="List_PLM_Journal_Master">主表List</param>
        /// <param name="List_PLM_Journal">子表List</param>
        /// <param name="Index">主表List选中行ID</param>
        private void DataGridView2_Refresh(List<PLM_Journal_Master> List_PLM_Journal_Master, List<PLM_Journal> List_PLM_Journal,string Index)
        {
            List<PLM_Journal_select_detail> List_PLM_Journal_select_detail = new List<PLM_Journal_select_detail>();
            List<PLM_Journal_select> List_PLM_Journal_select = new List<PLM_Journal_select>();

            List_PLM_Journal_select = Delete_duplicates(List_PLM_Journal_Master);
            List_PLM_Journal_select = List_PLM_Journal_select.Where(a => a.id== Convert.ToInt32(Index)).ToList();

            List_PLM_Journal_select_detail = (from PLM_Journal in List_PLM_Journal
                                              join PLM_Journal_select in List_PLM_Journal_select
                                              on new { PLM_Journal.Interface_name, PLM_Journal.Interface_parameter, PLM_Journal.error_message } equals new { PLM_Journal_select.Interface_name, PLM_Journal_select.Interface_parameter, PLM_Journal_select.error_message }
                                              into x
                                              from PLM_Journal_select in x.DefaultIfEmpty()
                                              where PLM_Journal_select != null
                                              orderby PLM_Journal.Interface_name, PLM_Journal.Interface_parameter, PLM_Journal.error_message, PLM_Journal.time
                                              select new PLM_Journal_select_detail
                                              {
                                                  Interface_name = (PLM_Journal == null) ? null : PLM_Journal.Interface_name,
                                                  Interface_parameter = (PLM_Journal == null) ? null : PLM_Journal.Interface_parameter,
                                                  error_message = (PLM_Journal == null) ? null : PLM_Journal.error_message,
                                                  time = PLM_Journal.time,
                                              }).Distinct(new Compare<PLM_Journal_select_detail>(
                                                        delegate (PLM_Journal_select_detail x, PLM_Journal_select_detail y)
                                                        {
                                                            if (null == x || null == y) return false;
                                                            return x.Interface_name == y.Interface_name && x.Interface_parameter == y.Interface_parameter && x.error_message == y.error_message & x.time == y.time;
                                                        })).ToList();

            DataTable dt_PLM_Journal = new DataTable();
            dt_PLM_Journal = Helper.Transformation.Transformation.DataConvert.ListToDataTable(List_PLM_Journal_select_detail);

            if (exDataGridView2.Columns.Count == 0)
            {
                exDataGridView2.AddColumn("Interface_name", "模块名称");
                exDataGridView2.AddColumn("Interface_parameter", "传递参数");
                exDataGridView2.AddColumn("error_message", "错误信息");
                exDataGridView2.AddColumn("time", "记录时间");
                exDataGridView2.AddColumn("id", "id", 80, true, null, DataGridViewContentAlignment.TopLeft, null, false);
                exDataGridView2.AllowUserToAddRows = false;
            }

            exDataGridView2.DataSource = dt_PLM_Journal;
        }

        #endregion        

        private void button1_Click(object sender, EventArgs e)
        {

            #region 声明

           
            DataTable dt_PLM_Journal_Master = new DataTable();

            List<PLM_Journal_select> List_PLM_Journal_select = new List<PLM_Journal_select>();
            List<PLM_Journal_select_detail> List_PLM_Journal_select_detail = new List<PLM_Journal_select_detail>();

            #endregion

            #region 取数据

            List_PLM_Journal_Master.Clear();
            List_PLM_Journal.Clear();

            List_PLM_Journal_Master = PLM_Interface_Bll.select_PLM_Journal_Master();
            List_PLM_Journal = PLM_Interface_Bll.select_PLM_Journal();

            #endregion

            #region 选择的筛选项目

            if (checkBox3.Checked == true)
            {
                List<PLM_Journal_select_detail> List_PLM_Journal_select_detail1 = new List<PLM_Journal_select_detail>();
                List<PLM_Journal_select> List_PLM_Journal_select1 = new List<PLM_Journal_select>();

                List_PLM_Journal_select_detail1 = Delete_duplicates_detail_2(List_PLM_Journal);
                List_PLM_Journal_select_detail1 = List_PLM_Journal_select_detail1.Where(d => d.time >= dateTimePicker1.Value.Date && d.time <= Convert.ToDateTime(dateTimePicker2.Value.Date.ToString().Substring(0, 9) + " 23:59:59.999")).ToList();


                List_PLM_Journal_select1 = (from PLM_Journal_Master in List_PLM_Journal_Master
                                            join PLM_Journal_select_detail in List_PLM_Journal_select_detail1
                                            on new { PLM_Journal_Master.Interface_name, PLM_Journal_Master.Interface_parameter, PLM_Journal_Master.error_message } equals new { PLM_Journal_select_detail.Interface_name, PLM_Journal_select_detail.Interface_parameter, PLM_Journal_select_detail.error_message }
                                            into x
                                            from PLM_Journal_select_detail in x.DefaultIfEmpty()
                                            where PLM_Journal_select_detail != null
                                            orderby PLM_Journal_Master.Interface_name, PLM_Journal_Master.Interface_parameter, PLM_Journal_Master.error_message
                                            select new PLM_Journal_select
                                            {
                                                Interface_name = (PLM_Journal_Master == null) ? null : PLM_Journal_Master.Interface_name,
                                                Interface_parameter = (PLM_Journal_Master == null) ? null : PLM_Journal_Master.Interface_parameter,
                                                Error_num = (PLM_Journal_Master == null) ? 0 : PLM_Journal_Master.Error_num,
                                                error_message = (PLM_Journal_Master == null) ? null : PLM_Journal_Master.error_message,
                                                mail = (PLM_Journal_Master == null) ? 0 : PLM_Journal_Master.mail,
                                                mail_date = PLM_Journal_Master.mail_date,
                                                mailing_address = "",
                                                id = PLM_Journal_Master.id,
                                            }).Distinct(new Compare<PLM_Journal_select>(
                                                            delegate (PLM_Journal_select x, PLM_Journal_select y)
                                                            {
                                                                if (null == x || null == y) return false;
                                                                return x.Interface_name == y.Interface_name && x.Interface_parameter == y.Interface_parameter && x.error_message == y.error_message;
                                                            })).ToList();

                //List_PLM_Journal_select = List_PLM_Journal_select.Distinct().ToList();

                //List_PLM_Journal_select_detail = Delete_duplicates_detail(List_PLM_Journal);
                List_PLM_Journal_Master = Delete_duplicates(List_PLM_Journal_select1);
            }

            if (checkBox5.Checked == true)
            {
                //checkBox3_string = "d.time >= " + dateTimePicker1.Value.ToString() + " && d.time <= " + dateTimePicker2.Value.ToString() ;
                List_PLM_Journal_Master = List_PLM_Journal_Master.Where(d => d.mail_date >= dateTimePicker3.Value.Date && d.mail_date <= Convert.ToDateTime(dateTimePicker4.Value.Date.ToString().Substring(0, 9) + " 23:59:59.999")).ToList();


                //List_PLM_Journal = Delete_duplicates_detail(List_PLM_Journal_select_detail);

            }

            if (checkBox4.Checked == true)
            {
                //checkBox3_string = "d.time >= " + dateTimePicker1.Value.ToString() + " && d.time <= " + dateTimePicker2.Value.ToString() ;
                List_PLM_Journal_Master = List_PLM_Journal_Master.Where(d => d.Interface_name == this.comboBox1.Text.ToString()).ToList();
            }

            if (checkBox1.Checked == true)
            {
                //checkBox3_string = "d.time >= " + dateTimePicker1.Value.ToString() + " && d.time <= " + dateTimePicker2.Value.ToString() ;
                List_PLM_Journal_Master = List_PLM_Journal_Master.Where(d => (d.error_message.ToString().Contains(textBox1.Text.ToString()))).ToList();
            }

            if (checkBox2.Checked == true)
            {
                //checkBox3_string = "d.time >= " + dateTimePicker1.Value.ToString() + " && d.time <= " + dateTimePicker2.Value.ToString() ;
                List_PLM_Journal_Master = List_PLM_Journal_Master.Where(d => (d.Interface_parameter.ToString().Contains(textBox2.Text.ToString()))).ToList();
            }

            #endregion

            #region 转DT显示

            dt_PLM_Journal_Master = Helper.Transformation.Transformation.DataConvert.ListToDataTable(List_PLM_Journal_Master);

            if (this.exDataGridView1.Columns.Count == 0)
            {
                exDataGridView1.AddColumn("Interface_name", "模块名称");
                exDataGridView1.AddColumn("Interface_parameter", "传递参数");
                exDataGridView1.AddColumn("error_message", "错误信息");
                exDataGridView1.AddColumn("Error_num", "错误次数");
                exDataGridView1.AddColumn("mail_date", "发送邮件日期");
                exDataGridView1.AddColumn("mail", "是否发送邮件");
                exDataGridView1.AddColumn("id", "id",80,true,null, DataGridViewContentAlignment.TopLeft,null, false);
                exDataGridView1.AddColumn("mailing_address", "mailing_address", 80, true, null, DataGridViewContentAlignment.TopLeft, null, false);
                exDataGridView1.AddColumn("plm_journal", "plm_journal", 80, true, null, DataGridViewContentAlignment.TopLeft, null, false);
                exDataGridView1.AllowUserToAddRows = false;
            }

            //exDataGridView1.Rows.Clear();
            exDataGridView1.DataSource = dt_PLM_Journal_Master;

            if (dt_PLM_Journal_Master.Rows.Count != 0)
            {
                DataGridView2_Refresh(List_PLM_Journal_Master, List_PLM_Journal, exDataGridView1.Rows[0].Cells["id"].Value.ToString());
            }
            else
            {
                DataGridView2_Refresh(List_PLM_Journal_Master, List_PLM_Journal, "0");
            }

            #endregion

        }

        #region exDataGridView1单击事件

        private void exDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView2_Refresh(List_PLM_Journal_Master, List_PLM_Journal, exDataGridView1.Rows[this.exDataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString());
        }

        #endregion

    }

    #region 比较器声明

    public delegate bool EqualsComparer<T>(T x, T y);

    public class Compare<T> : IEqualityComparer<T>
    {
        private EqualsComparer<T> _equalsComparer;

        public Compare(EqualsComparer<T> equalsComparer)
        {
            this._equalsComparer = equalsComparer;
        }

        public bool Equals(T x, T y)
        {
            if (null != this._equalsComparer)
                return this._equalsComparer(x, y);
            else
                return false;
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    #endregion

}
