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
        private void ToggleEnableDeleteButton(bool state)
        {
            buttonDelete.Enabled = state;
        }

        private void ToggleEnableNewButton(bool state)
        {
            buttonNew.Enabled = state;
        }

        private void ToggleEnableAllGroupBoxState(bool state)
        {
            groupBoxFilterByCountry.Enabled = state;
            groupBoxSearchBy.Enabled = state;
            groupSearchInput.Enabled = state;
        }

        private void ToggleEnableTextBoxState(bool state)
        {
            textBoxMemberCity.Enabled = state;
            textBoxMemberCountry.Enabled = state;
            textBoxMemberId.Enabled = state;
            textBoxMemberPassword.Enabled = state;
            textBoxMemberName.Enabled = state;
            textBoxMemberEmail.Enabled = state;
        }
        private void LoadFullListMembers()
        {
            dataGridViewMembers.DataSource = null;
            textBoxMemberId.DataBindings.Clear();
            textBoxMemberCity.DataBindings.Clear();
            textBoxMemberCountry.DataBindings.Clear();
            textBoxMemberPassword.DataBindings.Clear();
            textBoxMemberName.DataBindings.Clear();
            textBoxMemberEmail.DataBindings.Clear();

            _listMembers = MemberRepository.GetMembersList();
            _listMembers = from member in _listMembers orderby member.MemberName descending select member;
            _bindingSource.DataSource = _listMembers;

            dataGridViewMembers.DataSource = _bindingSource;
            textBoxMemberId.DataBindings.Add("Text", _bindingSource, "MemberId");
            textBoxMemberCity.DataBindings.Add("Text", _bindingSource, "City");
            textBoxMemberCountry.DataBindings.Add("Text", _bindingSource, "Country");
            textBoxMemberPassword.DataBindings.Add("Text", _bindingSource, "Password");
            textBoxMemberName.DataBindings.Add("Text", _bindingSource, "MemberName");
            textBoxMemberEmail.DataBindings.Add("Text", _bindingSource, "Email");
        }
        private void ReloadData()
        {
            LoadFullListMembers();
            if (_listMembers != null && _listMembers.Any())
            {
                ToggleEnableAllGroupBoxState(true);
                ToggleEnableDeleteButton(true);
                ToggleEnableNewButton(true);
                _bindingSource.DataSource = from member in _listMembers orderby member.MemberName descending select member;

                _cityFilter = (from member in _listMembers select member.City).Distinct().ToList();
                _cityFilter.Insert(0, "");
                comboBoxFilterByCity.DataSource = _cityFilter;

                _countryFilter = (from member in _listMembers select member.Country).Distinct().ToList();
                _countryFilter.Insert(0, "");
                comboBoxFilterByCountry.DataSource = _countryFilter;
            }
            else
            {
                ToggleEnableAllGroupBoxState(false);
                ToggleEnableDeleteButton(false);
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
