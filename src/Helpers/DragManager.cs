/* Used for storing a column or taskcard during dragging, needed for avalonias 
 * new DataTranfer() function as taskcards and columns are my objects and are 
 * not strings or ints. */
namespace App.Helpers;

public static class DragManager {

    public static object? DraggedItem { get; set; }

}
