import {
    AspectRatio,
    Button,
    ButtonGroup,
    Card,
    CardBody,
    CardFooter,
    Divider,
    Heading,
    Image,
    Stack,
    Text,
} from '@chakra-ui/react'
import NextLink from 'next/link'
import { useAppStates } from '../lib/AppContext'

function Item({ obj, ...props }) {
    const { action } = useAppStates()
    return (
        <Card
            maxW="sm"
            transitionDuration={'0.3s'}
            _hover={{ boxShadow: '1px 1px 1px 1px grey' }}
            {...props}
        >
            <CardBody>
                <AspectRatio ratio={1}>
                    <Image
                        src={obj["ImageURL"]}
                        alt="Green double couch with wooden legs"
                        borderRadius="lg"
                        objectFit='cover'
                    />
                </AspectRatio>
                <Stack mt="6">
                    <Heading size="md">{obj['Name']}</Heading>
                    <Text>
                        Left: {obj['AmountLeft']}
                    </Text>
                    <Text color="blue.600" fontSize={['md', 'xl']}>
                        VND {obj['Price']}
                    </Text>
                </Stack>
            </CardBody>
            <CardFooter>
                <Stack width="full" direction={{ base: "column", xl: "row" }}>
                    <Button colorScheme="blue" width="full"
                        as={NextLink}
                        href={`/meals/${obj["Id"]}`}>
                        Detail
                    </Button>
                    <Button colorScheme="purple" width="full" onClick={() => action.addToCart(obj)}>
                        Add to cart
                    </Button>
                </Stack>
            </CardFooter>
        </Card>
    )
}

export default Item
