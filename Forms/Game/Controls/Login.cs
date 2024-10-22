using KolmRakendust.Forms.Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Controls
{
    public delegate void SuccesfulSubmitDelegate(User user);
    public partial class Login: UserControl
    {
        
        public event SuccesfulSubmitDelegate OnSuccesfulSubmit;

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

        public EventHandler SuccesfulSubmit { get; set; }
        private DataManagment DM { get; set;}
        public const int UserControlWidth = 400;
        public const int UserControlHeight = 400;
        public TextBox UserNameInput { get; set; } = new TextBox 
        { 
            Font = new Font("Arial", 18),
            Name = "UserNameInput",
            Size = new Size(250, 50),
            PlaceholderText = "Type your username"
        };
        public TextBox PasswordInput { get; set; } = new TextBox
        {
            Font = new Font("Arial", 18),
            Name = "PasswordInput",
            Size = new Size(250, 50),
            PlaceholderText = "Type your password"
        };

        public Login()
        {
            this.DM = DataManagment.Instance;

            this.Width = UserControlWidth;
            this.Height = UserControlHeight;

            UserNameInput.Location = new Point((UserControlWidth / 2) - UserNameInput.Width / 2, 100);
            PasswordInput.Location = new Point((UserControlWidth / 2) - PasswordInput.Width / 2, 200);

            Label userNameInputLabel = new Label
            {
                Text = "Username",
                Font = new Font("Arial", 18),
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(250, 50)
            };
            userNameInputLabel.Location = new Point((UserControlWidth / 2) - userNameInputLabel.Width / 2, 50);

            Label passwordInputLabel = new Label
            {
                Text = "Password",
                Font = new Font("Arial", 18),
                Size = new Size(250, 50),
                TextAlign = ContentAlignment.MiddleLeft,
            };
            passwordInputLabel.Location = new Point((UserControlWidth / 2) - passwordInputLabel.Width / 2, 150);

            Button submitButton = new Button
            {
                Text = "Submit",
                Font = new Font("Arial", 18),
                Size = new Size(150, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            submitButton.Location = new Point((UserControlWidth / 2) - submitButton.Width / 2, UserControlHeight - submitButton.Height);
            submitButton.Click += new EventHandler(submit_Click);

            foreach (Control control in new List<Control>(){ UserNameInput, PasswordInput, userNameInputLabel, passwordInputLabel, submitButton }){
                this.Controls.Add(control);
            }
        }
        private void submit_Click(object? sender, EventArgs e)
        {
            User? user = DM.GetUserByName(UserNameInput.Text);
            if (user is not null)
            {
                if(user.Password == PasswordInput.Text)
                {
                    
                    OnSuccesfulSubmit(user);
                    MessageBox.Show("You're logged into the account!", "Login form");
                }
                else
                {
                    MessageBox.Show("Wrong password!", "Login form");
                }
            }
            else
            {
                if (UserNameInput.Text == "" || PasswordInput.Text == "")
                {
                    MessageBox.Show("Change your username or password!", "Login form");
                    return;
                }

                User newUser = new User(UserNameInput.Text, PasswordInput.Text);
                DM.AddUser(newUser);
                
                OnSuccesfulSubmit(newUser);
                MessageBox.Show("You've been registered!", "Login form");
            }

        }
    }
}
