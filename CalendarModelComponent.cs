using ILBNew.Common;
using ILBNew.Web.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace {Your NameSpace}
{
    public class CalendarModel : IValidatableObject
    {
        public CalendarModel(string modelName)
        {
            ModelName = modelName;
        }
        public string Now { get; set; } = "";
        [RegularExpression("^[1][1-4][0-9]{2}\\/((0[1-6]\\/(0[1-9]|[1-2][0-9]|3[0-1]))|(0[7-9]\\/(0[1-9]|[1-2][0-9]|30))|(1[0-1]\\/(0[1-9]|[1-2][0-9]|30))|(12\\/(0[1-9]|[1-2][0-9]|30)))", ErrorMessage = "فرمت {0} درست نیست")]
        public string FromDate { get; set; } = "";
        [RegularExpression("^[1][1-4][0-9]{2}\\/((0[1-6]\\/(0[1-9]|[1-2][0-9]|3[0-1]))|(0[7-9]\\/(0[1-9]|[1-2][0-9]|30))|(1[0-1]\\/(0[1-9]|[1-2][0-9]|30))|(12\\/(0[1-9]|[1-2][0-9]|30)))", ErrorMessage = "فرمت {0} درست نیست")]
        public string ToDate { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام مدل داده ای کامپوننت وارد نشده (ModelName)")]
        public string ModelName { get; set; }

        public bool IsFromDateRequired { get; set; }
        public bool IsToDateRequired { get; set; }
        public bool IsSingleDatePicker { get; set; }

        public bool IsFromDateGreaterThanNow { get; set; }
        public bool IsFromDateGreaterOrEqualThanNow { get; set; }

        public bool IsToDateGreaterOrEqualThanNow { get; set; }
        public bool IsToDateGreaterThanNow { get; set; }

        public bool hasError { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime fromDate = FromDate.ToGregorianDate();
            DateTime toDate = ToDate.ToGregorianDate();
            if (IsFromDateRequired && IsToDateRequired && string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(ToDate))
            {
                yield return new ValidationResult("وارد کردن تاریخ شروع الزامی است", new[] { nameof(FromDate) });
                yield return new ValidationResult("وارد کردن تاریخ پایان الزامی است", new[] { nameof(ToDate) });
            }
            else if (IsFromDateRequired && !IsToDateRequired && string.IsNullOrEmpty(FromDate))
            {
                yield return new ValidationResult("وارد کردن تاریخ شروع الزامی است", new[] { nameof(FromDate) });
            }
            else if (IsToDateRequired && !IsFromDateRequired && string.IsNullOrEmpty(ToDate))
            {
                yield return new ValidationResult("وارد کردن تاریخ پایان الزامی است", new[] { nameof(ToDate) });
            }
            if (IsFromDateGreaterOrEqualThanNow && fromDate < DateTime.Today)
            {
                yield return new ValidationResult("تاریخ شروع نمی تواند قبل از تاریخ جاری باشد", new[] { nameof(FromDate)});
            }
            if (IsFromDateGreaterThanNow && fromDate <= DateTime.Today )
            {
                yield return new ValidationResult("تاریخ شروع نمی تواند قبل از تاریخ جاری باشد", new[] { nameof(FromDate) });
            }
            if (IsToDateGreaterOrEqualThanNow && toDate < DateTime.Today)
            {
                yield return new ValidationResult("تاریخ پایان نمی تواند قبل از تاریخ جاری باشد", new[] { nameof(ToDate) });
            }
            if (IsToDateGreaterThanNow && toDate <= DateTime.Today)
            {
                yield return new ValidationResult("تاریخ پایان نمی تواند قبل از تاریخ جاری باشد", new[] { nameof(ToDate) });
            }
            if ((IsFromDateRequired||IsToDateRequired) && !IsSingleDatePicker && toDate <= fromDate )
            {
                yield return new ValidationResult("تاریخ پایان نمی تواند قبل از تاریخ شروع باشد", new[] { nameof(FromDate), nameof(ToDate) });
            }

        }
    }
}
