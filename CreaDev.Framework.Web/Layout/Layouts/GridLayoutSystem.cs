using System;

namespace CreaDev.Framework.Web.Layout.Layouts
{
    public abstract class GridLayoutSystem
    {
        private string _containerName = "container";
        private string _rowClassName = "row";
        private string _extraSmallColumnSizeName = "xs";
        private string _smallColumnSizeName = "sm";
        private string _mediumColumnSizeName = "md";
        private string _largeColumnSizeName = "lg";
        private string _columnName  = "col";
        private string _columnClassNameSeperator = "-";
        private string _columnClassFormat = $"columnName-columnSizeName-columnWidth";
        private string _columnClassOffsetFormat = $"columnName-columnSizeName-offset-columnWidth";
        private string _formControlCssClassName  = "form-control";
        private int MaxColumnWith = 12;

        public string FormControl { get { return _formControlCssClassName; } }

        public string Container {get { return _containerName; }}
        public string Row {get { return _rowClassName; } }

        public string AutoColumn(int largeColumnWidth)
        {
            throw new NotImplementedException();
        }
        public string Column(int? largeColumnWidth, int? mediumColumnWidth, int? smallColumnWidth, int? extraSmalColumnWidth)
        {
            string resultClasses = string.Empty;
            if (largeColumnWidth.HasValue)
            {
                resultClasses += Column(largeColumnWidth.Value, GridLayoutSystemColumnSize.Large)+" ";
            }
            if (mediumColumnWidth.HasValue)
            {
                resultClasses += Column(mediumColumnWidth.Value, GridLayoutSystemColumnSize.Medium) + " ";
            }
            if (smallColumnWidth.HasValue)
            {
                resultClasses += Column(smallColumnWidth.Value, GridLayoutSystemColumnSize.Small) + " ";
            }
            if (extraSmalColumnWidth.HasValue)
            {
                resultClasses += Column(extraSmalColumnWidth.Value, GridLayoutSystemColumnSize.ExtraSmall) + " ";
            }

            return resultClasses;
        }
        public string Column(int width, GridLayoutSystemColumnSize size)
        {
            string columnClassName = string.Empty;
            string columnSizeText = GetColumnSizeText(size);
            columnClassName = _columnClassFormat.Replace("columnName", _columnName).Replace("columnWidth", width.ToString()).Replace("columnSizeName", columnSizeText);
            return columnClassName;
        }
        public string Offset(int width, GridLayoutSystemColumnSize size)
        {
            string columnClassName = string.Empty;
            string columnSizeText = GetColumnSizeText(size);
            columnClassName = _columnClassOffsetFormat.Replace("columnName", _columnName).Replace("columnWidth", width.ToString()).Replace("columnSizeName", columnSizeText);
            return columnClassName;
        }

        private string GetColumnSizeText(GridLayoutSystemColumnSize size)
        {
            string columnSizeText = null;
            switch (size)
            {
                    case GridLayoutSystemColumnSize.ExtraSmall:
                    columnSizeText =_extraSmallColumnSizeName;
                    break;

                case GridLayoutSystemColumnSize.Small:
                    columnSizeText = _smallColumnSizeName;
                    break;

                case GridLayoutSystemColumnSize.Medium:
                    columnSizeText = _mediumColumnSizeName;
                    break;

                case GridLayoutSystemColumnSize.Large:
                    columnSizeText = _largeColumnSizeName;
                    break;

            }

            return columnSizeText;
        }
    }
}
