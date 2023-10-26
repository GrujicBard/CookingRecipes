export default class User {
    id?: number;
    userName: string | null;
    email: string | null;
    password: string | null;

    constructor(
        id: number,
        userName: string,
        email: string,
        password: string
    ) {
        this.id = id
        this.userName = userName
        this.email = email
        this.password = password
    }


}