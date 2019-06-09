import { Component, Inject } from "@angular/core";
import { DialogData } from "../shared/create-file-modal/shared-create-file-modal.component";
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from "@angular/material";
import { CommandClient, WriteFileCommand, Credentials } from "src/api";
import { EditFileModalData } from "src/entities/EditFileModalData";

@Component({
    selector: 'editor-modal-component',
    templateUrl: './editor-modal.component.html',
    styleUrls: ['./editor-modal.component.css']
})

export class EditorModalComponent {
    constructor(public dialogRef: MatDialogRef<EditorModalComponent>,
      private client: CommandClient,
      private snackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: EditFileModalData) {
      }
      Save(){
        let command = new WriteFileCommand();
        command.credentials = new Credentials(this.data.server);
        command.path = this.data.filePath + "/" + this.data.fileName;
        command.data = this.data.fileBody;
       this.client.writeFile(command).subscribe(data => {
          if(!data.isError){
            this.snackBar.open('Successfully saved', '', {
              duration: 2000,
           });
          }else{
            this.snackBar.open(data.error, '', {
              duration: 2000,
           });
          }
          this.dialogRef.close();
        })
      }
      onNoClick(): void {
        this.dialogRef.close();
      }
}