import { Component, OnInit, AfterViewChecked, ViewChild } from "@angular/core";
import { CommandClient, FileNode, Credentials, GetFilesCommand, CreateObjectCommand, DeleteObjectCommand } from "src/api";
import { ServersService } from "src/app/services/servers.service";
import { NavigationEnd } from "@angular/router";
import { MatMenuTrigger, MatDialog } from "@angular/material";
import { SharedCreateFileModel } from "../shared/create-file-modal/shared-create-file-modal";

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
   currentItem = new FileNode();
   @ViewChild('context') contextMenu;
   constructor(private serversService: ServersService,
      private client: CommandClient, public dialog: MatDialog) { }

   onRightClick(event, item) {
      event.preventDefault();
      this.currentItem = item;
      this.contextMenu.nativeElement.setAttribute("style", "position:absolute;visibility:visible;"
         + "top:" + event.clientY + "px;left:" + (event.clientX - 20) + "px");
   }

   onUnovering(event) {
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
      const dialogRef = this.dialog.open(SharedCreateFileModel, {
         width: '70vh',
         data: { name: '', isFile: isFile }
      });
      dialogRef.afterClosed().subscribe(result => {
         if (result.name) {
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
      let command = new DeleteObjectCommand();
      command.credentials = new Credentials(this.currentServer);
      command.path = this.pathList[this.currentPathIndex];
      command.name = this.currentItem.name;
      this.client.delete(command).subscribe(data => {
         this.UpdateData(data);
      });
   }

   OpenEditor() {

   }

   Copy() {

   }

   Paste() {

   }

   onChangeServer(event: Credentials) {
      this.currentServer = event;
      this.pathList = new Array<string>();
      this.currentPathIndex = 0;
      this.Navigate('');
   }
}  