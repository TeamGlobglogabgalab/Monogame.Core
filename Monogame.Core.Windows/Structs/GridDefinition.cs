using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Structs;

public struct GridDefinition
{
    public int rowCount { get; set; }
    public int columnCount { get; set; }

    public GridDefinition(int rowCount, int columnCount)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
    }
}
