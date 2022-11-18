# QACourse1Project - Part One

## Testing new Brand filter on www.target.com as applied to toilet paper products

### Confirm Brand filter of 1 brand
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Brand.
5. Select one brand (i.e. Cottonelle).
6. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected brand.*

### Confirm Brand filter of 2 brands
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Brand.
5. Select two brands (i.e. Cottonelle & Charmin).
6. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected brands.*

### Confirm Brand filter of 3 brands
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Brand.
5. Select three brands (i.e. Cottonelle, Charmin, & Scott).
6. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected brands.*

### Confirm adding additional brand to an existing Brand filter
**Critical Path Test**

*User must start on page showing toilet papers with at least one brand selected in the Brand filter.*
1. Press the Filter button.
2. Press Brand.
3. Select one of the unselected brands (leave any selected brands as is).
4. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected brands including the added brand.*

### Confirm removing a brand from existing Brand filter
**Critical Path Test**

*User must start on page showing toilet papers with at least two brands selected in the filter.*
1. Press the Filter button.
2. Press Brand.
3. Unselect one selected brands (leave at least one selected brand).
4. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results only removed the brand unselected.*

### Confirm unselecting all brands from existing Brand filter
**Critical Path Test**

*User must start on page showing toilet papers with at least one brand selected in the filter.*
1. Press the Filter button.
2. Press Brand.
3. Unselect all selected brands.
4. Press Update twice (once for Brand, once for All Filters).

*Confirm all results are being shown.*

### Confirm resetting Brand filter clears Brand filter
**Critical Path Test**

*User must start on page showing toilet papers with at least one brand selected in the filter.*
1. Press the Filter button.
2. Press Brand.
3. Press Reset to remove all filters.

*Confirm all results are being shown.*

### Confirm adding Brand filter to an existing non-Brand filter
**Critical Path Test**

*User must start on page showing toilet papers with one non-Brand filter selected (ie. Features, Septic Safe).*
1. Press the Filter button.
2. Press Brand.
3. Select one brand (i.e. Cottonelle).
4. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected brands from previously filtered results.*

### Confirm Brand filter using Brand filter button
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Brand button in Results header.
4. Select one brand (i.e. Cottonelle).
5. Press Update.

*Confirm filtered results show only the selected brand.*

### Confirm clearing Brand filter using Brand filter button
**Critical Path Test**

*User must start on page showing toilet papers with at least one brand selected in the filter.*
1. Press the Brand button in Results header.
2. Press Reset to remove all filters.

*Confirm all results are being shown.*

### Confirm Brand filter button matches Brand filter
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Brand.
5. Select one brand (i.e. Cottonelle).
6. Press Update twice (once for Brand, once for All Filters).
7. Press the Brand button in Results header.

*Confirm Brand filter opens showing only the Brand filter with the filtered brand selected.*

### Confirm Brand filter is cleared after leaving page
**Critical Path Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Brand.
5. Select one brand (i.e. Cottonelle).
6. Press Update twice (once for Brand, once for All Filters).
7. Type "paper towel" into the Search bar.
8. Press the Search button.

*Confirm no filters are selected.*

### Confirm non-Brand filters still work correctly
**Regression Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Filter button.
4. Press Type.
5. Select Toilet Paper.
6. Press Update twice (once for Brand, once for All Filters).

*Confirm filtered results show only the selected type (personal wipes should not show).*

### Confirm non-Brand filter button still works correctly
**Regression Test**

*User must start from main page with no current filters selected.*
1. Type "toilet paper" into the Search bar.
2. Press the Search button.
3. Press the Type button in Results header.
4. Select Toilet Paper.
5. Press Update.

*Confirm filtered results show only the selected type (personal wipes should not show).*
