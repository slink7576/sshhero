import { Injectable } from "@angular/core";
import { Credentials } from "src/api";

@Injectable()
export class ServersService {
    getServers() {
        return JSON.parse(localStorage.getItem('servers'));
    }
    addServer(server: Credentials) {
        let serv = JSON.parse(localStorage.getItem('servers'));
        if (!serv) {
            serv = new Array<Credentials>();
        }
        serv.push(server);
        localStorage.setItem('servers', JSON.stringify(serv));

    }
    deleteServer(server: Credentials) {
        let serv = JSON.parse(localStorage.getItem('servers'));
        if (serv) {
            serv.forEach(element => {
                if(element.hostname == server.hostname){
                    serv.splice(serv.indexOf(element), 1);
                }
            });
            localStorage.setItem('servers', JSON.stringify(serv));
        }

    }
}