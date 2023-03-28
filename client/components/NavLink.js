import { Link } from '@chakra-ui/react';
import NextLink from 'next/link'

const NavLink = ({ href, children, ...props }) => (
    <Link
        as={NextLink}
        href={href}
        {...props}
    >
        {children}
    </Link>
)

export default NavLink;