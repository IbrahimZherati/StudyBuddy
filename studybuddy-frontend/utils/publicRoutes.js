const publicRoutes = ["/", "/login", "/register", "/confirm_account"];

export const isPublicRoute = (route) => publicRoutes.includes(route)