using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class FormMemberManagement : Form
    {
        private IMemberRepository MemberRepository { get; init; }
        private readonly BindingSource _bindingSource = new BindingSource();

        private IEnumerable<MemberObject>? _listMembers;
        private IEnumerable<MemberObject>? _searchResult;
        private List<string>? _cityFilter;
        private List<string>? _countryFilter;
        public FormMemberManagement(IMemberRepository memberRepository)
        {
            MemberRepository = memberRepository;
            InitializeComponent();
        }
    }
}
