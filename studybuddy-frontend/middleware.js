import { NextResponse } from "next/server"

const publicRoutes = ["/", "/login", "/register"];

const isPublicRoute = (route) => publicRoutes.includes(route)

export function middleware(request) {
    const { pathname } = request.nextUrl;

    const token = request.cookies.get(".AspNetCore.Identity.Application");

    if(!isPublicRoute(pathname) && !token) {
        const loginUrl = new URL("/login", request.url);
        loginUrl.searchParams.set("callbackUrl", pathname);

        return NextResponse.redirect(loginUrl);
    }

    return NextResponse.next();
}

export const config = {
    matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"]
}