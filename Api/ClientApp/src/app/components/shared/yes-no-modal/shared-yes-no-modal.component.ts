import { Component, Input, Output, EventEmitter, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

@Component({
  selector: 'shared-yes-no-modal-component',
  templateUrl: './shared-yes-no-modal.component.html'
})

export class SharedYesNoModalComponent {
  constructor(public dialogRef: MatDialogRef<SharedYesNoModalComponent>) {
  }
}
