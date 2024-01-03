using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Structs;

public struct GridIndex
{
    public int rowIndex { get; set; }
    public int columnIndex { get; set; }

    public GridIndex(int rowIndex, int columnIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
    }
}
