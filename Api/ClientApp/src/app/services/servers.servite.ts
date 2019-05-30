import { Injectable } from "@angular/core";
import { Credentials } from "src/api";

@Injectable()
export class ServersService {
    getServers() {
        return localStorage.getItem('servers');
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
            const index = serv.indexOf(server, 0);
            if (index > -1) {
                serv.splice(index, 1);
            }
            localStorage.setItem('servers', JSON.stringify(serv));
        }

    }
}