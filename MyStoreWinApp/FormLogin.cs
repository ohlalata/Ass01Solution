using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class FormLogin : Form
    {
        private readonly IMemberRepository _memberRepository = new MemberRepository();
        public FormLogin()
        {
            InitializeComponent();
        }
        private void buttonLogin_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string email = textBoxEmail.Text.Trim();
                string password = textBoxPassword.Text.Trim();
                bool isAdminLoginSuccess = false;

                if (email.Length == 0 || password.Length == 0)
                {
                    bool isClose = MessageBox.Show(@"Missing email or password.", @"Login Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel;
                    if (isClose)
                    {
                        Close();
                    }
                    return;
                }

                MemberObject adminAccount = _memberRepository.GetDefaultAdminAccount();
                if (email.Equals(adminAccount.Email) && password.Equals(adminAccount.Password))
                {
                    isAdminLoginSuccess = true;
                }

                if (isAdminLoginSuccess)
                {
                    FormMemberManagement formMemberManagement = new FormMemberManagement(_memberRepository);
                    this.Hide();
                    if (formMemberManagement.ShowDialog() == DialogResult.Cancel)
                    {
                        Close();
                    }
                    return;
                }

                MemberObject? memberAccount = _memberRepository.FindAccountMemberByEmail(email);
                if (memberAccount != null && memberAccount.Password.Equals(password))
                {
                    FormMemberDetails formMemberDetails = new FormMemberDetails(_memberRepository)
                    {
                        IsUpdateMode = true,
                        MemberParam = memberAccount
                    };
                    Hide();
                    DialogResult dialogResult = formMemberDetails.ShowDialog();
                    if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (MessageBox.Show(@"Incorrect email or password!", @"Login Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        Close();
                    }
                }

            }
            catch (Exception)
            {
                if (MessageBox.Show(@"System busy can't login now. Try again!", @"Login Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    Close();
                }
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
