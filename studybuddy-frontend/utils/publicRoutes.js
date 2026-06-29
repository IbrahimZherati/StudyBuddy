const publicRoutes = ["/", "/login", "/register", "/confirm_account", "/account_verified"];

export const isPublicRoute = (route) => publicRoutes.includes(route)