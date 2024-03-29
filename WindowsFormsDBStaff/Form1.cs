﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDBStaff
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            using (var db = new DBStafEntities())
            {
                var Customer = db.Customers.Select(c => new { c.Name, Phone = c.Telephone_Number, ID = c.CustomerId });
                dataGridView1.DataSource = Customer.ToList();
                db.SaveChanges();
            }

            using (var db = new DBStafEntities())
            {
                var Order = db.Orders.Select(o => new
                {
                    Data = o.OrderDate,
                    Product = o.Product_Description,
                    id = o.OrderId
                });
                dataGridView2.DataSource = Order.ToList();
            }

            using (var db = new DBStafEntities())
            {
                var Cars = db.Cars.Select(o => new { o.Marka, o.Owner, o.Price, id = o.CarId });
                dataGridView4.DataSource = Cars.ToList();
            }

        }






        //        using (var db = new DBStafEntities())
        //        {
        //            var query = db.Customers.Select(c => new
        //            {
        //                ID_order = c.CustomerId,
        //                c.Name,
        //                Phone = c.Telephone_Number,


        //            });
        //    dataGridView1.DataSource = query.ToList();
        //         }

        //        using (var db = new DBStafEntities())
        //        {
        //            var query = db.Orders.Select(o => new
        //            {
        //                ID_customer = o.OrderId,
        //                Data = o.OrderDate,
        //                Product = o.Product_Description
        //            });
        //dataGridView2.DataSource = query.ToList();
        //        }



        //        using (DBStafEntities db = new DBStafEntities())
        //        {
        //            var Query = db.Customers.Join(db.Orders, customers => customers.CustomerId, orders => orders.OrderId,
        //                ((customers, orders) => new
        //                {
        //                    Name = customers.Name,
        //                    Phone = customers.Telephone_Number,

        //                    Data = orders.OrderDate,
        //                    Product = orders.Product_Description
        //                }));
        //dataGridView1.DataSource = Query.ToList();
        //        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (var db = new DBStafEntities())
            {
                var Query = from customers in db.Customers
                    join cars in db.Cars on customers.CustomerId equals cars.CarId // Добавленны данные  по таблицам
                    join orders in db.Orders on customers.CustomerId equals orders.OrderId
                    select new
                    {
                        customers.Name,
                        Phone = customers.Telephone_Number,
                        Id = customers.CustomerId,
                        cars.Marka,
                        cars.Price,
                        cars.Owner,
                        Data = orders.OrderDate,
                        Product = orders.Product_Description,
                        CustomerId = orders.CustomerID,
                        orders.OrderId
                    };
                dataGridView3.DataSource = Query.ToArray();
            }
        }

        private void Button3_Click(object sender, EventArgs e) // Добавленна вывод данных из таблиц в TreeView
        {
            for (var i = 0; i < dataGridView3.Rows.Count; i++)
            {
                var node = new TreeNode("Данные");
                for (var j = 0; j < dataGridView3.Columns.Count; j++)
                    node.Nodes.Add(dataGridView3.Rows[i].Cells[j].Value.ToString());
                treeView1.Nodes.Add(node);
            }
        }
        
    }





        //var Query = db.Customers.Join(db.Orders, customers => customers.CustomerId, orders => orders.OrderId,
        //    (customers, orders) => new
        //    {
        //        Name = customers.Name,
        //        Phone = customers.Telephone_Number,
        //        Data = orders.OrderDate,
        //        Product = orders.Product_Description,
        //        ID = customers.CustomerId,
        //        id = orders.OrderId,

        //    });
        //dataGridView3.DataSource = Query.ToArray();

    }
    




        //private void button3_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < dataGridView3.Rows.Count; i++)
        //    {
        //        TreeNode node = new TreeNode("Данные");
        //        for (int j = 0; j < dataGridView3.Columns.Count; j++)
        //        {
        //            node.Nodes.Add(dataGridView3.Rows[i].Cells[j].Value.ToString());
        //        }

        //        treeView1.Nodes.Add(node);
        //    }
        //}

      
    



    

