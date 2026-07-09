using WinFormsApp1.Data;

namespace WinFormsApp1.Forms
{
    public partial class Form1 : Form
    {
        private readonly IUnitOfWork _unitOfWork;

        // DI constructor
        public Form1(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        // Parameterless constructor for designer
        public Form1()
        {
            InitializeComponent();
        }
    }
}
