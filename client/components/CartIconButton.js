import { Avatar, AvatarBadge, IconButton, Text } from '@chakra-ui/react'
import { FiShoppingCart } from 'react-icons/fi'
import NextLink from 'next/link'
import { useAppStates } from '@/lib/AppContext'
import { useState } from 'react'

function CartIconButton() {
    const { cart } = useAppStates()

    return (
        <>
            <IconButton
                aria-label={'Cart'}
                as={NextLink}
                size={'lg'}
                href="/cart"
                icon={
                    <>
                        <Avatar
                            color={'inherit'}
                            bg={'transparent'}
                            icon={<FiShoppingCart fontSize={32} />}
                        >
                            {!!cart.length && (
                                <AvatarBadge
                                    placement="top-end"
                                    boxSize={['1rem', null, '1.2rem']}
                                    bg={'purple.300'}
                                    borderWidth={0}
                                    zIndex={9999}
                                >
                                    <Text fontSize={['0.6rem', null, '0.8rem']}>
                                        {cart.length}
                                    </Text>
                                </AvatarBadge>
                            )}
                        </Avatar>
                    </>
                }
            />
        </>
    )
}

export default CartIconButton
