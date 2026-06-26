import { NextResponse } from "next/server"

const publicRoutes = ["/", "/login", "/register", "/confirm_account"];

const isPublicRoute = (route) => publicRoutes.includes(route)

export async function proxy(request) {
    const { pathname } = request.nextUrl;

    const token = request.cookies.get(".AspNetCore.Identity.Application");

    if (!isPublicRoute(pathname) && !token) {
        const loginUrl = new URL("/login", request.url);
        loginUrl.searchParams.set("callbackUrl", pathname);

        return NextResponse.redirect(loginUrl);
    }
    else if (pathname.startsWith("/chat/")) {
        const clientId = pathname.split("/").at(-1);

        const response = await fetch(
            `http://localhost:5203/api/ClientUser/GetProfileByClientId?clientId=${clientId}`, {
            headers: {
                Cookie: request.headers.get("cookie") || "",
            },
        }
        );

        const data = await response.json();
        const userProfile = data.value;

        if (!(userProfile?.isFriend)) {
            const profileUrl = new URL(`/profile/${clientId}`, request.url);
            return NextResponse.redirect(profileUrl);
        }
    }

    return NextResponse.next();
}

export const config = {
    matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"]
}