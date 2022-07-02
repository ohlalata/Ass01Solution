using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class FormMemberDetails : Form
    {
        public bool IsUpdateMode { get; init; }
        private IMemberRepository MemberRepository { get; init; }
        public bool IsCreateMode { get; init; }
        public MemberObject? MemberParam { get; init; }
        public FormMemberDetails(IMemberRepository memberRepository)
        {
            MemberRepository = memberRepository;
            InitializeComponent();
        }
    }
}
