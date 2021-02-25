import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationService } from 'src/app/auth/auth.service';

@Component({
    selector: 'app-topnav',
    templateUrl: './topnav.component.html',
    styleUrls: ['./topnav.component.scss']
})
export class TopnavComponent implements OnInit {
    public pushRightClass: string;
    public fullName: string;
    public date: string;

    constructor(public router: Router, private auth: AuthenticationService,
        private translate: TranslateService) {
        this.router.events.subscribe(val => {
            if (val instanceof NavigationEnd && window.innerWidth <= 992 && this.isToggled()) {
                this.toggleSidebar();
            }
        });

        setInterval(() => {
            const currentDate = new Date();
            this.date = currentDate.toLocaleString();
        }, 1000);
    }

    ngOnInit() {
        this.pushRightClass = 'push-right';
        this.fullName = this.auth.getFullName();
    }

    isToggled(): boolean {
        const dom: Element = document.querySelector('body');
        return dom.classList.contains(this.pushRightClass);
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle(this.pushRightClass);
    }

    onLoggedout() {
        this.auth.logout().subscribe(
            res => {
                if (res) {
                    this.router.navigate(['/login']);
                    this.auth.saveLogout(null).subscribe(
                        (res) => {

                        }
                    );
                }
            });
    }

    changeLang(language: string) {
        this.translate.use(language);
    }
}
