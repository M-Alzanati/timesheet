import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from 'src/app/auth/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogData, MessageBoxComponent } from '../components/message-dialog/message-dialog-component';

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
    timeSheetDisplayedColumns = ['date', 'loginTime', 'logoutTime'];
    timeSheetDataSource = new MatTableDataSource([]);

    displayedColumns = ['time'];
    loginDataSource = new MatTableDataSource([]);
    logoutDataSource = new MatTableDataSource([]);

    firstLogin: string;
    lastLogout: string;

    timeSheetForm: FormGroup = new FormGroup({
        date: new FormControl(new Date()),
        loginTime: new FormControl(new Date().toTimeString()),
        logoutTime: new FormControl(new Date().toTimeString())
    });

    constructor(private auth: AuthenticationService, private dialog: MatDialog) {

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

        this.auth.getTimeSheet().subscribe(
            (res) => {
                this.timeSheetDataSource = new MatTableDataSource(res);
            });
    }

    applyFilter(filterValue: string) {
        filterValue = filterValue.trim();
        filterValue = filterValue.toLowerCase();
        this.loginDataSource.filter = filterValue;
        this.logoutDataSource.filter = filterValue;
        this.timeSheetDataSource = new MatTableDataSource([]);
    }

    onFormSubmit() {
        this.auth.saveTimeSheet({
            Date: this.timeSheetForm.get('date')?.value,
            LoginTime: this.timeSheetForm.get('loginTime')?.value,
            LogoutTime: this.timeSheetForm.get('logoutTime')?.value,
            UUId: this.auth.getUUId()
        }).subscribe(
            (res) => {
                if (res) {
                    let msg: DialogData = { title: 'Success', content: 'TimeSheet Submitted Successfully' };
                    this.dialog.open(MessageBoxComponent, { data: msg });
                    
                    this.auth.getTimeSheet().subscribe(
                        (res) => {
                            this.timeSheetDataSource = new MatTableDataSource(res);
                        });
                }
            }
        )
    }
}
