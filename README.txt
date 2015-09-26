Authors:
	Justin Greenlee
	Adam Hammond

1. Summary:
	Application enables tree-styled view of photos
	and photo editing options.

2. Bugs:

3. Extra Credit:

4. Contributions:

5. Percentages:

6. TODO's:
DONE-JG	1.) Create Menu Strip with stubs for all clickable action under each menu (i.e. File > Locate on Disc, View > Details, etc.)
DONE-JG	2.) Create 2 separate panels in Main Window (1 to hold Directory Treeview and the 2nd to hold the image thumbnails within current directory)
DONE-JG	3.) Create Tree View for left panel in Main Window which will be used to display directories on user's machine and allow user to click to access images within that respective directory.
DONE-JG	4.) Add Functionality for Current user's "Pictures" directory to open by default in Directory TreeView upon Program startup.
DONE-JG	5.) Create ListView for right panel in Main Window which will be used to display image thumbnails for all images in the currently selected directory represented by the selecte node in the directory TreeView.
DONE-JG	6.) Add Functionality for all images in the directory represented by the currently selected treeView node to be populated as thumbnail images in the Image Thumbnail ListView panel in Main Window
DONE-JG	7.) Add an "indeterminate" progress bar that displays in the bottom-left of the Main Window while the image thumbnails are being loaded into the (Right) Image Thumbnail ListView panel for the currently selected directory.
DONE-JG	8.) Make it to where the Main Window is still responsive while the image thumbnails are being loaded into the (Right) Image Preview/ListView panel.
DONE-JG	9.) Add functionality for user to Click File > Select Root Folder to change the current Root Folder in the directory Tree View
	10.) Add "Large" View Option so when user clicks View > Large, all of the images in the currently selected directory will be displayed in "Large" format.
	11.) Add "Small" View Option so when user clicks View > Small, all of the images in the currently selected directory will be displayed in "Small" format.
	12.) Add "Details" View Option so when user clicks View > Details, all of the images in the currently selected directory will be displayed in "Details" format.
	13.) Create an "About" Dialog Box Which will open when the user clicks Help > About
	14.) Create "Edit Photo" Form with button stubs which will be used by the user to actually edit the photos
	15.) Add functionality for user to double-click an image thumbnail in the image thumbnail ListView panel, and automatically open the "Edit Photo" form with the double-clicked image as the active image in the "Edit Photo" form.
	16.) Add functionality for user to edit the image brightness in the "Edit Photo" form.
	17.) Add functionality for user to edit the image color in the "Edit Photo" form.
	18.) Add functionality for user to invert the image in the "Edit Photo" form.
	19.) Create a Progress Bar Dialog Box which will display when a photo-editing operation is being performed in the "Edit Photo" form.
	20.) Add functinality for the Progress Bar Dialog Box to display when a photo-editing operation is being performed (i.e. brightness, color, invert), which will display the progress of the current operation and also keep the application RESPONSIVE while the expensive operation is being performed.
	21.) Add functionality for user to click the "Cancel" button in the "Edit Photo" form while an photo-editing operation is being performed and the operation cancels; REVERTING ANY CHANGES made to the active photo.
	22.) Add functionality for the "Edit Photo" form to display the newly transformed image in place of the old image AFTER a photo-editing operation has completed WITHOUT THE USER PRESSING THE CANCEL BUTTON.
	23.) Add functionality for all of the "clickable" controls (i.e. buttons, meters, etc.) to be disabled while a photo-editing operation is being performed (with the exception of the "Cancel" button of coarse).
	24.) Add functionality for user to press the "Save" button and the currently transformed image will OVERWRITE the old image in memory.
	25.) Add functionality to where Alt-F4 or pressing the close button on the "Edit Photo" form will have the same result as if the user had pressed the "Cancel" button on the "Edit Photo" form.
	26.) Add an icon for the application that will appear in the Windows task bar and all windows title bars for all forms.