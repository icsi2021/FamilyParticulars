function Validation(Object)
{
var ObjEventStartDate = document.getElementById('ctl00_IMIS_Event_Start_Date');
var ObjEventEndDate = document.getElementById('ctl00_IMIS_Event_End_Date');

 if ((ObjEventStartDate.value != "") && (ObjEventEndDate.value != ""))
 {
        var dtSDate = ConvertTextToDate(ObjEventStartDate.value);
        var dtEDate = ConvertTextToDate(ObjEventEndDate.value);
        if (dtSDate.setDate(dtSDate.getDate()) > dtEDate.setDate(dtEDate.getDate())) {
            DateMessage();
            Object.focus();
            return false;
        }
  }
    
}
function DateMessage()
{
    alert('Event End Date should be greater than Event Start Date');
}
function ConvertTextToDate(strDate) {
    if (typeof (strDate) != 'undefined' && strDate != null) {
        return new Date(strDate.split('/')[2], strDate.split('/')[1] - 1, strDate.split('/')[0]);
    }
}