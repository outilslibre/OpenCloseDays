# OpenCloseDays

The goal of this project is to provide :

- a common access to public holidays date and names (`PublicHolidaysService` class)
- a common access to compute open and close days and work hours for a  given period (`OpenCloseDaysService` class) including: 
	- date exclusion, 
	- public holidays exclusion for a given country,
	- external source of school holidays

This library uses following NuGet packages :

- Public Holidays (Portugal and Brazil) : [Holidays](https://github.com/reboucaslinhares/Holidays)
- Public Holidays (other countries) : [PublicHoliday](https://github.com/martinjw/Holiday)
