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
    }
}
