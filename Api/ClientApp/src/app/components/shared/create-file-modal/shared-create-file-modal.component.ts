
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Inject, Component } from "@angular/core";

@Component({
  selector: 'shared-create-file-modal-component',
  templateUrl: './shared-create-file-modal.component.html'
})

export class SharedCreateFileModalComponent {
  constructor(public dialogRef: MatDialogRef<SharedCreateFileModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {

  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}

export interface DialogData {
  name: string;
  isFile: boolean;
}