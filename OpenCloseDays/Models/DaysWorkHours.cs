namespace OpenCloseDays.Models
{
    public class DaysWorkHours
    {
        public DaysWorkHours()
        {
        }

        public DaysWorkHours(
            int mondayWorkHours, int mondayWorkMinutes, 
            int tuesdayWorkHours, int tuesdayWorkMinutes, 
            int wednesdayWorkHours, int wednesdayWorkMinutes, 
            int thursdayWorkHours, int thursdayWorkMinutes, 
            int fridayWorkHours, int fridayWorkMinutes, 
            int saturdayWorkHours = 0, int saturdayWorkMinutes = 0, int sundayWorkHours = 0, int sundayWorkMinutes = 0)
        {
            MondayWorkHours = mondayWorkHours;
            MondayWorkMinutes = mondayWorkMinutes;
            TuesdayWorkHours = tuesdayWorkHours;
            TuesdayWorkMinutes = tuesdayWorkMinutes;
            WednesdayWorkHours = wednesdayWorkHours;
            WednesdayWorkMinutes = wednesdayWorkMinutes;
            ThursdayWorkHours = thursdayWorkHours;
            ThursdayWorkMinutes = thursdayWorkMinutes;
            FridayWorkHours = fridayWorkHours;
            FridayWorkMinutes = fridayWorkMinutes;
            SaturdayWorkHours = saturdayWorkHours;
            SaturdayWorkMinutes = saturdayWorkMinutes;
            SundayWorkHours = sundayWorkHours;
            SundayWorkMinutes = sundayWorkMinutes;
        }

        public int MondayWorkHours { get; set; } = 7;
        public int TuesdayWorkHours { get; set; } = 7;
        public int WednesdayWorkHours { get; set; } = 7;
        public int ThursdayWorkHours { get; set; } = 7;
        public int FridayWorkHours { get; set; } = 7;
        public int SaturdayWorkHours { get; set; } = 0;
        public int SundayWorkHours { get; set; } = 0;

        public int MondayWorkMinutes { get; set; } = 0;
        public int TuesdayWorkMinutes { get; set; } = 0;
        public int WednesdayWorkMinutes { get; set; } = 0;
        public int ThursdayWorkMinutes { get; set; } = 0;
        public int FridayWorkMinutes { get; set; } = 0;
        public int SaturdayWorkMinutes { get; set; } = 0;
        public int SundayWorkMinutes { get; set; } = 0;

        public float HoursPerWeek => MondayWorkHours
            + TuesdayWorkHours + WednesdayWorkHours
            + ThursdayWorkHours + FridayWorkHours
            + SaturdayWorkHours + SundayWorkHours
            + (MondayWorkMinutes
            + TuesdayWorkMinutes + WednesdayWorkMinutes
            + ThursdayWorkMinutes + FridayWorkMinutes
            + SaturdayWorkMinutes + SundayWorkMinutes) / 60.0f;
    }
}
