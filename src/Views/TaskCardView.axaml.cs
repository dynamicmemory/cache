using Avalonia.Controls;
using App.ViewModels;
using Avalonia.Input;

namespace App.Views;

public partial class TaskCardView : UserControl {

    public TaskCardView() {
        InitializeComponent();
    }

    private void EditTask(object sender, PointerPressedEventArgs e) {
        if (DataContext is TaskCardViewModel vm)
            vm.OpenEditorCommand.Execute(null);
    }

}
