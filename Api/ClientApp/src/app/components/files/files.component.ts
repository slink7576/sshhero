import { Component, OnInit, AfterViewChecked } from "@angular/core";
import { CommandClient, FileNode, Credentials, GetFilesCommand } from "src/api";
import { ServersService } from "src/app/services/servers.service";
import { NavigationEnd } from "@angular/router";


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


   constructor(private serversService: ServersService,
      private client: CommandClient) { }
onBack(){
   if(this.currentPathIndex != 0){
      this.currentPathIndex--;
   }else{
      let path = '/';
      console.log(this.pathList[0].split('/'))
      for(let i = 0; i < this.pathList[0].split('/').length - 1; i++){
        if(this.pathList[0].split('/')[i] != ''){
         path+= this.pathList[0].split('/')[i];
        }
      }
      this.pathList.unshift(path);
      this.currentPathIndex = 0;
   }
   this.pathList.splice(this.pathList.length - 1, 1)
   this.Navigate(this.pathList[this.currentPathIndex]);
}

onNodeClick(node: FileNode){
   if(!node.isFile){
      let path = this.pathList[this.currentPathIndex] == '/' 
         ? '/' + node.name : this.pathList[this.currentPathIndex] + '/' + node.name
      this.pathList.push(path);
      this.currentPathIndex = this.pathList.length - 1;
      this.Navigate(this.pathList[this.currentPathIndex]);
   }
}

onPathChange(event){
   this.pathList.push(event.srcElement.value);
   this.currentPathIndex = this.pathList.length - 1;
   this.Navigate(this.pathList[this.currentPathIndex]);
}

Navigate(path){
   
   let command = new GetFilesCommand();
   command.credentials = new Credentials(this.currentServer);
   command.path = path.length == 0 ? '' : path;
   this.client.getFiles(command).subscribe(data => {
      if (!data.isError) {
         if(path.length == 0){
            this.pathList.push(data.path);
         }
         this.error = '';
         if(data.nodes.length == 0){
            this.error = 'The folder is empty';
            this.filesFirst = new Array<FileNode>();
            this.filesSecond = new Array<FileNode>();
         }else{
            this.filesFirst = data.nodes.splice(0, data.nodes.length/2);
            this.filesSecond = data.nodes.splice(data.nodes.length/2, data.nodes.length);
         } 
      } else {
         this.error = data.error;
         this.filesFirst = new Array<FileNode>();
         this.filesSecond = new Array<FileNode>();
      }
   });
}


   onChangeServer(event: Credentials) {
      this.currentServer = event;
      this.pathList = new Array<string>();
      this.currentPathIndex = 0;
      this.Navigate('');
   }
}  