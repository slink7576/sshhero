import { Component, OnInit, AfterViewChecked, ViewChild } from "@angular/core";
import { CommandClient, FileNode, Credentials, GetFilesCommand, CreateObjectCommand, DeleteObjectCommand, GetFileBodyCommand, CutObjectCommand, CopyObjectCommand } from "src/api";
import { ServersService } from "src/app/services/servers.service";
import { NavigationEnd } from "@angular/router";
import { MatMenuTrigger, MatDialog, MatSnackBar } from "@angular/material";
import { SharedCreateFileModalComponent } from "../shared/create-file-modal/shared-create-file-modal.component";
import { SharedYesNoModalComponent } from "../shared/yes-no-modal/shared-yes-no-modal.component";
import { EditorModalComponent } from "../editor/editor-modal.component";
import { EditFileModalData } from "src/entities/EditFileModalData";

@Component({
   selector: 'files-component',
   templateUrl: './files.component.html',
   styleUrls: ['./files.component.css']
})

export class FilesComponent {
   filesFirst = new Array<FileNode>();
   filesSecond = new Array<FileNode>();
   currentServer = new Credentials();
   pathList = new Array<string>();
   currentPathIndex = 0;
   error = '';
   currentItem = null;
   fileNameBuffer = '';
   filePathBuffer = '';
   isCut = false;
   @ViewChild('context') contextMenu;
   constructor(private serversService: ServersService,
      private snackBar: MatSnackBar,
      private client: CommandClient, public dialog: MatDialog) { }

   onOpenMenu(event){
      event.preventDefault();
      this.contextMenu.nativeElement.setAttribute("style", "position:absolute;visibility:visible;"
      + "top:" + (event.clientY + 40)  + "px;left:" + (event.clientX - 40) + "px");
   }


   onRightClick(item) {
      this.currentItem = item;
   }

   onUnovering(event) {
      this.currentItem = null;
      this.contextMenu.nativeElement.setAttribute("style", "position:absolute;visibility:hidden;");
   }

   onBack() {
      if (this.currentPathIndex != 0) {
         this.currentPathIndex--;
      } else {
         let path = '/';
         for (let i = 0; i < this.pathList[0].split('/').length - 1; i++) {
            if (this.pathList[0].split('/')[i] != '') {
               path += this.pathList[0].split('/')[i];
            }
         }
         this.pathList.unshift(path);
         this.currentPathIndex = 0;
      }
      this.pathList.splice(this.pathList.length - 1, 1)
      this.Navigate(this.pathList[this.currentPathIndex]);
   }

   onNodeClick(node: FileNode) {
      if (!node.isFile) {
         let path = this.pathList[this.currentPathIndex] == '/'
            ? '/' + node.name : this.pathList[this.currentPathIndex] + '/' + node.name
         this.pathList.push(path);
         this.currentPathIndex = this.pathList.length - 1;
         this.Navigate(this.pathList[this.currentPathIndex]);
      } else {

      }
   }

   onPathChange(event) {
      this.pathList.push(event.srcElement.value);
      this.currentPathIndex = this.pathList.length - 1;
      this.Navigate(this.pathList[this.currentPathIndex]);
   }

   Navigate(path) {
      let command = new GetFilesCommand();
      command.credentials = new Credentials(this.currentServer);
      command.path = path.length == 0 ? '' : path;
      this.client.getFiles(command).subscribe(data => {
         if (!data.isError) {
            if (path.length == 0) {
               this.pathList.push(data.path);
            }
            this.UpdateData(data);
         }
      });
   }

   UpdateData(data) {
      if (!data.isError) {
         this.error = '';
         if (data.nodes.length == 0) {
            this.error = 'The folder is empty';
            this.filesFirst = new Array<FileNode>();
            this.filesSecond = new Array<FileNode>();
         } else {
            this.filesFirst = data.nodes.slice(0, data.nodes.length / 2);
            this.filesSecond = data.nodes.slice(data.nodes.length / 2, data.nodes.length);
         }
      } else {
         this.error = data.error;
         this.filesFirst = new Array<FileNode>();
         this.filesSecond = new Array<FileNode>();
      }
   }

   Create(isFile) {
      const dialogRef = this.dialog.open(SharedCreateFileModalComponent, {
         width: '70vh',
         data: { name: '', isFile: isFile }
      });
      dialogRef.afterClosed().subscribe(result => {
         if (result) {
            let command = new CreateObjectCommand();
            command.credentials = new Credentials(this.currentServer);
            command.isFile = isFile;
            command.name = result.name,
               command.path = this.pathList[this.currentPathIndex];
            this.client.create(command).subscribe(data => {
               this.UpdateData(data);
            });
         }
      });
   }

   Delete() {
      let name = this.currentItem.name;
      const dialogRef = this.dialog.open(SharedYesNoModalComponent, {
         width: '70vh'
      });
      dialogRef.afterClosed().subscribe(result => {
         if(result){
            let command = new DeleteObjectCommand();
            command.credentials = new Credentials(this.currentServer);
            command.path = this.pathList[this.currentPathIndex];
            command.name = name;
            this.client.delete(command).subscribe(data => {
               this.UpdateData(data);
            });
         }
      });
   }

   OpenEditor() {
      let name = this.currentItem.name;
      let command = new GetFileBodyCommand();
      command.credentials = new Credentials(this.currentServer);
      command.path = this.pathList[this.currentPathIndex] + '/' + this.currentItem.name;
      this.client.getFileBody(command).subscribe(data => {
         if(!data.isError){
            let modalData = new EditFileModalData()
            modalData.fileName = name;
            modalData.filePath = this.pathList[this.currentPathIndex];
            modalData.fileBody = data.data;
            modalData.server = this.currentServer;
            const dialogRef = this.dialog.open(EditorModalComponent, {
               width: '100vh',
               data: modalData
            });
            dialogRef.afterClosed().subscribe(result => {
   
            })
         }
        else{
            this.snackBar.open(data.error, '', {
               duration: 2000,
            });
        }
      })
   }

   Cut() {
      this.fileNameBuffer = this.currentItem.name;
      this.filePathBuffer = this.pathList[this.currentPathIndex];
      this.isCut = true;
   }

   Copy() {
      this.fileNameBuffer = this.currentItem.name;
      this.filePathBuffer = this.pathList[this.currentPathIndex];
      this.isCut = false;
   }

   Paste() {
      if(this.fileNameBuffer.length > 1){
         if(this.isCut){
            let command = new CutObjectCommand();
            command.credentials = new Credentials(this.currentServer);
            command.file = this.fileNameBuffer;
            command.from = this.filePathBuffer;
            command.to = this.pathList[this.currentPathIndex];
            this.client.cut(command).subscribe(data => {
               this.UpdateData(data);
            });
         }else{
            let command = new CopyObjectCommand();
            command.credentials = new Credentials(this.currentServer);
            command.file = this.fileNameBuffer;
            command.from = this.filePathBuffer;
            command.to = this.pathList[this.currentPathIndex];
            this.client.copy(command).subscribe(data => {
               this.UpdateData(data);
            });
         }
      }
      this.fileNameBuffer = '';
   }

   onChangeServer(event: Credentials) {
      this.currentServer = event;
      this.pathList = new Array<string>();
      this.currentPathIndex = 0;
      this.Navigate('');
   }
}  