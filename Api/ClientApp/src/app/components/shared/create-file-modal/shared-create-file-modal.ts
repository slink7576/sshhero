import { Component, Input, Output, EventEmitter, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

@Component({
  selector: 'shared-create-file-modal',
  templateUrl: './shared-create-file-modal.html'
})

export class SharedCreateFileModel {
  constructor(public dialogRef: MatDialogRef<SharedCreateFileModel>,
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