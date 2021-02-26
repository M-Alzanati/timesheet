import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from 'src/app/auth/auth.service';

export interface PeriodicElement {
    name: string;
    time: string;
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    displayedColumns = ['time'];
    loginDataSource = new MatTableDataSource([]);
    logoutDataSource = new MatTableDataSource([]);
    firstLogin: string;
    lastLogout: string;

    constructor(private auth: AuthenticationService) {

    }

    ngOnInit() {
        this.auth.getFirstLogin().subscribe(
            (data) => {
                this.firstLogin = data;
            }
        );

        this.auth.getLastLogout().subscribe(
            (data) => {
                this.lastLogout = data;
            }
        );

        this.auth.getLogins().subscribe(
            (data: any) => {
                this.loginDataSource = data;
            }
        );

        this.auth.getLogouts().subscribe(
            (data: any) => {
                this.logoutDataSource = data;
            }
        );
    }

    applyFilter(filterValue: string) {
        filterValue = filterValue.trim();
        filterValue = filterValue.toLowerCase();
        this.loginDataSource.filter = filterValue;
        this.logoutDataSource.filter = filterValue;
    }
}
