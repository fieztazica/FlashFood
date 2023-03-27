import { Button, ButtonGroup, Card, CardBody, CardFooter, Divider, Heading, Image, Stack, Text } from "@chakra-ui/react"

function Item() {
    return <Card maxW='sm' transitionDuration={"0.3s"} _hover={{ cursor: "pointer", boxShadow: "1px 1px 1px 1px grey" }}>
        <CardBody>
            <Image
                src='https://images.unsplash.com/photo-1555041469-a586c61ea9bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80'
                alt='Green double couch with wooden legs'
                borderRadius='lg'
            />
            <Stack mt='6' spacing='3'>
                <Heading size='md'>Living room Sofa</Heading>
                <Text color='blue.600' fontSize='2xl'>
                    $450
                </Text>
            </Stack>
        </CardBody>
    </Card>
}

export default Item