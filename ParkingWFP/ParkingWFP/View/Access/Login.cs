﻿/* *************************************************************************************************
*                       Copyright © 2018 MYF Sotwares. All rights reserved. 
* *********************************************************************************************** */

using ParkingWFP.Model;
using System;
using System.Windows.Forms;

namespace ParkingWFP.View.Access
{
    public partial class Login : Form
    {
        public User user = new User();
        public bool isAdmin = false;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cbx_users.DataSource = user.LoadUsersToList();
            cbx_users.DisplayMember = "Username";
            cbx_users.ValueMember = "Username";
            cbx_users.SelectedIndex = 0;
        }

        private void LoadUser()
        {
            var selected = cbx_users.SelectedValue;
            if (selected == null || selected.GetType() == typeof(User))
            {
                return;
            }

            user = user.LoadUserByUsername(selected.ToString());
        }
        private void cbx_users_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadUser();
        }

        public bool isValidUser(User user)
        {
            var password = txb_password.Text.Trim();

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show($"Senha é um campo obrigatório");
                return false;
            }

            if (user.Password != password)
            {
                MessageBox.Show($"A senha digitada está incorreta");
                return false;
            }
            return true;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            LoadUser();
            if (isValidUser(user))
            {
                isAdmin = true;
                Close();
            }
        }

        private void txb_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;

            if ((Keys)e.KeyChar == Keys.Enter)
            {
                LoadUser();
                if (isValidUser(user))
                {
                    isAdmin = true;
                    Close();
                }
                return;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
