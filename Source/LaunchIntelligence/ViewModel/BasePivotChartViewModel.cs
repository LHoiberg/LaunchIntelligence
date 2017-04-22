using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Filtering;
using Telerik.Pivot.Queryable;
using Telerik.Pivot.Queryable.Filtering;

namespace LaunchIntelligence.ViewModel
{
    public class BasePivotChartViewModel : ViewModelBase
    {
        private Model.LaunchTrackerEntities _dataContext;

        public BasePivotChartViewModel()
        {
            Init();
            Title = "Ad hoc analysis";
            Subtitle = "Pivot table";


        }

        private void Init()
        {
            _dataContext = new Model.LaunchTrackerEntities();
            DataProvider = new QueryableDataProvider();
            SetupPivot();
            DataProvider.Source = _dataContext.LI_BI_MonthlyIms.AsQueryable<Model.LI_BI_MonthlyIms>();

            System.Console.WriteLine(_dataContext.LI_BI_MonthlyIms.Count());

            PivotChartViewModel = new PivotChartViewModel();
            PivotChartViewModel.DataProvider = DataProvider;

   
            
        }

        private void SetupPivot()
        {
            //YEAR
            QueryableDateTimeGroupDescription yearGroupDescription = new QueryableDateTimeGroupDescription();
            yearGroupDescription.PropertyName = "Date";
            yearGroupDescription.Step = DateTimeStep.Year;

            //MONTH
            QueryableDateTimeGroupDescription monthGroupDescription = new QueryableDateTimeGroupDescription();
            monthGroupDescription.PropertyName = "Date";
            monthGroupDescription.Step = DateTimeStep.Month;

            //COUNTRY
            QueryablePropertyFilterDescription countryGroupDescription = new QueryablePropertyFilterDescription();
            countryGroupDescription.PropertyName = "Country";

            var countryDistinctCondition = new QueryableSetCondition();
            countryDistinctCondition.Comparison = SetComparison.Includes;
            countryDistinctCondition.Items.Add("Germany");

            var countryCondition = new QueryableItemsFilterCondition();
            countryCondition.DistinctCondition = countryDistinctCondition;

            countryGroupDescription.Condition = countryCondition;

            //VALUE TYPE
            QueryablePropertyFilterDescription valueTypeGroupDescription = new QueryablePropertyFilterDescription();
            valueTypeGroupDescription.PropertyName = "ValueType";

            var valueTypeDistinctCondition = new QueryableSetCondition();
            valueTypeDistinctCondition.Comparison = SetComparison.Includes;
            valueTypeDistinctCondition.Items.Add("DKK");

            var valueTypeCondition = new QueryableItemsFilterCondition();
            valueTypeCondition.DistinctCondition = valueTypeDistinctCondition;

            valueTypeGroupDescription.Condition = valueTypeCondition;

            //PRODUCT
            QueryablePropertyGroupDescription productGroupDescription = new QueryablePropertyGroupDescription();
            productGroupDescription.PropertyName = "Product";

            var productDistinctCondition = new SetCondition();
            productDistinctCondition.Comparison = SetComparison.Includes;
            productDistinctCondition.Items.Add("Xultophy®");
            //productDistinctCondition.Items.Add("Tresiba®");

            var productCondition = new ItemsFilterCondition();
            productCondition.DistinctCondition = productDistinctCondition;

            var labelGroupFilter = new LabelGroupFilter();
            labelGroupFilter.Condition = productCondition;

            productGroupDescription.GroupFilter = labelGroupFilter;

            //QueryablePropertyGroupDescription brandGroupDescription = new QueryablePropertyGroupDescription();
            //brandGroupDescription.PropertyName = "Brand";

            QueryablePropertyAggregateDescription amountDescription = new QueryablePropertyAggregateDescription();
            amountDescription.PropertyName = "Value";
            amountDescription.StringFormat = "#,##0,,M";
            amountDescription.AggregateFunction = QueryableAggregateFunction.Sum;

            DataProvider.RowGroupDescriptions.Add(yearGroupDescription);
            DataProvider.RowGroupDescriptions.Add(monthGroupDescription);
            DataProvider.ColumnGroupDescriptions.Add(productGroupDescription);
            DataProvider.FilterDescriptions.Add(countryGroupDescription);
            DataProvider.FilterDescriptions.Add(valueTypeGroupDescription);
            DataProvider.AggregateDescriptions.Add(amountDescription);
        }


        private PivotChartViewModel _pivotChartViewModel;
        public PivotChartViewModel PivotChartViewModel
        {
            get { return _pivotChartViewModel; }
            set { Set(() => PivotChartViewModel, ref _pivotChartViewModel, value); }
        }


        private QueryableDataProvider _dataProvider;
        public QueryableDataProvider DataProvider
        {
            get { return _dataProvider; }
            set { Set(() => DataProvider, ref _dataProvider, value); }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        private bool _isFieldListVisible = false;
        public bool IsFieldListVisible
        {
            get { return _isFieldListVisible; }
            set { Set(() => IsFieldListVisible, ref _isFieldListVisible, value); }
        }



        private string _subtitle;
        public string Subtitle
        {
            get { return _subtitle; }
            set { Set(() => Subtitle, ref _subtitle, value); }
        }



    }
}
