import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
    {
        name: 'Dashboard',
        url: '/dashboard',
        icon: 'fa fa-home',
    },
    {
        name: 'Add New',
        url: '/add-family',
        icon: 'fa-solid fa-file-circle-plus',
    },
    {
        name: 'Houses',
        url: '/houses',
        icon: 'fa fa-list',
    },
    {
        name: 'Members',
        url: '/members',
        icon: 'fa fa-users',
    },
    {
        name: 'Wasam Map',
        url: '/branch-map',
        icon: 'fa fa-map',
    },
    {
        name: 'Admin Setup',
        url: '/admin',
        icon: 'fa fa-gear',
        children: [
            {
                name: 'Officers Config',
                url: '/officer-config',
            },
            {
                name: 'Audit Log',
                url: '/audit-log',
            }
        ]
    }
    
];