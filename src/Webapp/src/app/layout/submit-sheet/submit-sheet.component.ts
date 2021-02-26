import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from 'src/app/auth/auth.service';

export interface PeriodicElement {
    name: string;
    time: string;
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './submit-sheet.component.html',
    styleUrls: ['./submit-sheet.component.scss']
})
export class SubmitSheetComponent implements OnInit {
    displayedColumns = ['time'];

    constructor(private auth: AuthenticationService) {

    }

    ngOnInit() {

        this.auth.getLogouts().subscribe(
            (data: any) => {
                
            }
        );
    }

    applyFilter(filterValue: string) {
        filterValue = filterValue.trim();
        filterValue = filterValue.toLowerCase();
        // this.loginDataSource.filter = filterValue;
    }
}
