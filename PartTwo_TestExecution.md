# QACourse1Project - Part Two

## Defect Report
## Header filter selections do not match side bar filter selections

### Date Found: 10/18/2022
### Organization: Code Louisville
### Identified By: Cheryl Draper

This is occurring in desktop browsers Chrome and Edge. It does not occur on mobile browsers.
This is an open, outstanding issue.
Problem was identified after the last round of production updates on 10/05/2022.
It is present in the live, production environment.

This defect is not preventing customers from using the website but will cause a negative user experience.
Customers are still able to use the filters on the sidebar and ignore the header filters.

*Steps to Reproduce
1. Start from main page with no current filters selected
2. Type "toilet paper" into the Search bar.
3. Press the Search button.
4. Press the Filter button.
5. Press Brand.
6. Select one brand (i.e. Cottonelle).
7. Press Update twice (once for Brand, once for All Filters).
8. Brand filter on Header row will show a different brand.
